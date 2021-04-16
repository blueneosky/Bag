#!/usr/bin/env python3
import signal
import time
import traceback
import pathlib
import threading
from configparser import ConfigParser

from alphonse_config import AlphonseConfig, \
    CONF_LOG_SECTION_NAME, CONF_LOG_FILE_NAME, CONF_LOG_WITH_DEBUG, CONF_LOG_WITH_TRACE
from alphonse_logger import Logger
from alphonse_modem import Modem, MODEM_NUMBER_TAG, MODEM_INIT, MODEM_RESET
from alphonse_phone_handlers import PhoneNumberHandlerContext, CallHistoryHandler, \
    BlacklistHandler, WhitelistHandler

DEBUG_CONFIG_PATH = "./alphonse.conf.local"
CONFIG_PATH = "/etc/alphonse.conf"


# - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
class Decoder:

    def __init__(self, log: Logger, *phone_number_handlers):
        self._buffer: str = ""
        self._log = log
        self._handlers = phone_number_handlers

    def append(self, data: str):
        self._log.trace(">> decoder.append: " + data.replace('\r', '\\r').replace('\n', '\\n'))
        self._buffer += data
        carriage_return_index = self._buffer.find('\r')
        while carriage_return_index > -1:
            leading_line_feed = self._buffer[0] == '\n'
            msg: str = self._buffer[(1 if leading_line_feed else 0):carriage_return_index]
            self._buffer = self._buffer[carriage_return_index + 1:len(self._buffer)]
            # self.log.trace(">> decoder.extract: " + msg)
            if msg:
                self._log.trace("> decoder.msg: " + msg)
                self._process(msg)
            carriage_return_index = self._buffer.find('\r')

    def _process(self, data: str):
        if str == "OK":
            self._log.trace("process.ignored: " + data)
            return
        if str == "RING":
            self._log.info("RING tone received")
            return

        tag_index = data.find(MODEM_NUMBER_TAG)
        if tag_index < 0:
            self._log.trace("process.ignored: " + data)
            return  # other values ignored

        tag_index += len(MODEM_NUMBER_TAG)
        number = data[tag_index:len(data)]
        if len(number) == 0:
            return
        self._log.info("Call from [{n}]".format(n=number))
        self._process_number(number)

    def _process_number(self, number: str):
        context = PhoneNumberHandlerContext(number)
        for handler in self._handlers:
            handler.process(context)  # got do some quacks
            if context.should_stop_processing:
                break

        return


# - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
class Alphonse:

    def __init__(self, log: Logger, config: AlphonseConfig):
        self._log = log
        self._config = config
        self._halt_requested = False
        self._reset_requested = False
        self._cancellation_requested = False

    @property
    def halt_requested(self):
        return self._halt_requested

    @property
    def reset_requested(self):
        return self._reset_requested

    def _listening(self, modem: Modem):
        # init modem com
        self._log.info(
            f'--- Alphonse on {modem.name}  {modem.baudrate},{modem.bytesize},{modem.parity},{modem.stopbits} ---\n')
        self._log.trace("  flush input...")
        modem.reset_input_buffer()

        self._log.trace("  reset modem")
        modem.write_command(MODEM_RESET)

        self._log.trace("  setup modem")
        modem.write_command(MODEM_INIT)

        # init processing
        call_history_handler = CallHistoryHandler(self._log, self._config)
        blacklist_handler = BlacklistHandler(modem, self._log, self._config)
        whitelist_handler = WhitelistHandler(modem, self._log, self._config)

        decoder = Decoder(self._log, call_history_handler, blacklist_handler, whitelist_handler)
        while not self._cancellation_requested:
            data = modem.read(modem.in_waiting or 1)
            if data:
                decoder.append(data.decode("utf-8"))
            time.sleep(0.1)  # await 100ms

    def listening(self):
        self._halt_requested = False
        self._reset_requested = False

        # open com
        self._log.trace("Open modem...")
        modem = Modem(self._config.modem_device, 9600, timeout=1.0)  # not in try/except : crash quick without repeat

        try:
            self._listening(modem)
        except Exception as err:
            self._log.error(f"Unexpected error append: {err}")
            self._log.debug(traceback.format_exc())
        finally:
            if modem is not None:
                modem.close()

    def signal_handler(self, signum, frame):
        self._log.info(f"SIGNAL receive: {signal.strsignal(signum)}")
        if signum == signal.SIGUSR1:
            self._log.info("Reset signal received")
            self._reset_requested = True
        else:
            self._log.info("[goodbye cruel world]")
            self._halt_requested = True
        self._cancellation_requested = True


# - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
class SignalDispatcher:

    def __init__(self):
        self._lock = threading.RLock()
        self._handlers = []

    def subscribe(self, handler):
        """
        expect that handler have a method like this signal_handler(signum, frame)
        """
        with self._lock:
            self._handlers.append(handler)

    def unsubscribe(self, handler):
        with self._lock:
            self._handlers.remove(handler)

    def __iadd__(self, other):
        self.subscribe(other)
        return self

    def __isub__(self, other):
        self.subscribe(other)
        return self

    def clear(self):
        with self._lock:
            self._handlers.clear()

    def dispatch(self, signum, frame):
        with self._lock:
            handlers = self._handlers.copy()
        for handler in handlers:
            handler.signal_handler(signum, frame)


# - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
_signal_dispatcher = SignalDispatcher()


def signal_handler(signum, frame):
    _signal_dispatcher.dispatch(signum, frame)


def alphonse_main(signal_dispatcher: SignalDispatcher):
    log = Logger()
    config = None
    try:
        _config_path = None
        if pathlib.Path(DEBUG_CONFIG_PATH).is_file():
            _config_path = DEBUG_CONFIG_PATH
        elif pathlib.Path(CONFIG_PATH).is_file():
            _config_path = CONFIG_PATH
        else:
            log.error(f"No config file found at {CONFIG_PATH}")
            exit(-1)

        config_parser = ConfigParser()
        config_parser.read_file(open(_config_path))

        # --- log initialisation ---
        _log_file_name = config_parser.get(CONF_LOG_SECTION_NAME, CONF_LOG_FILE_NAME)
        _log_with_debug = config_parser.getboolean(CONF_LOG_SECTION_NAME, CONF_LOG_WITH_DEBUG)
        _log_with_trace = config_parser.getboolean(CONF_LOG_SECTION_NAME, CONF_LOG_WITH_TRACE)
        log = Logger(_log_file_name, _log_with_debug, _log_with_trace)

        log.info("Starting...")
        log.debug(f"Usage of config file from {_config_path}")

        # some configuration check
        config = AlphonseConfig(log, config_parser)
        config.load()
    except Exception as _err:
        log.err(f"Fail to read configuration: {_err}")
        log.debug(traceback.format_exc())
        exit(-2)

    # signal registration
    signal.signal(signal.SIGINT, signal_handler)    # ctrl+c, for logging
    signal.signal(signal.SIGTERM, signal_handler)   # kill, for logging
    signal.signal(signal.SIGUSR1, signal_handler)   # reset on SIGUSR1

    alphonse = None
    while alphonse is None or not alphonse.halt_requested:
        alphonse = Alphonse(log, config)
        signal_dispatcher += alphonse
        alphonse.listening()
        signal_dispatcher.clear()

        if alphonse.halt_requested:
            break
        if alphonse.reset_requested:
            log.info("Reset in progress...")
        else:
            # something went wrong and we need to restart the listening
            log.info("Restarting...")

    return


# - - - - - start point - - - - - -

if __name__ == "__main__":
    try:
        alphonse_main(_signal_dispatcher)
    except Exception as err:
        log = Logger("/var/log/alphonse")
        log.err(f"Global fail: {err}")
        log.debug(traceback.format_exc())
