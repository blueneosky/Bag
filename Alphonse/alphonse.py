#!/usr/bin/env python3
import daemon
import getopt
import sys
import signal
import time
import traceback
import serial
import pathlib
import threading
from configparser import ConfigParser
from datetime import datetime, timedelta
from typing import List


# don't forget, for voice mode https://en.wikipedia.org/wiki/Voice_modem_command_set


DEBUG_CONFIG_PATH = "./alphonse.conf.local"
CONFIG_PATH = "/etc/alphonse.conf"
CONF_SERVICE_SECTION_NAME = "SERVICE"
CONF_SERVICE_MODEM_DEVICE = "MODEM_DEVICE"
CONF_SERVICE_WHITELIST_FILE = "WHITELIST_FILE"
CONF_SERVICE_BLACKLIST_FILE = "BLACKLIST_FILE"
CONF_LOG_SECTION_NAME = "LOG"
CONF_LOG_FILE_NAME = "LOG_FILE"
CONF_LOG_WITH_DEBUG = "WITH_DEBUG"
CONF_LOG_WITH_TRACE = "WITH_TRACE"

MODEM_RESET = [b"ATZ"]  # reset
MODEM_INIT = [b"ATE0", b"AT+VCID=1"]  # no echo, activate call id printing
MODEM_PICKUP = [b"ATA"]  # will provide a nice variations of modem strident sound
MODEM_HANGUP = [b"ATH"]
MODEM_NUMBER_TAG = "NMBR = "


# - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
class Modem(serial.Serial):

    def write_command(self, commands: List[bytes]):
        data = b''.join(map(lambda cmd: cmd + b'\r\n', commands))
        super().write(data)


# - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
class Logger:
    def __init__(self, file_path: str = None, with_debug: bool = True, with_trace: bool = True):
        self._file_path = file_path
        self._with_debug = with_debug
        self._with_trace = with_trace

    def _msg(self, msg: str):
        print(msg)
        timestamp = datetime.now().strftime("%Y/%m/%d %H:%M:%S ")
        if self._file_path is None:
            return
        with open(self._file_path, "a") as logfile:
            logfile.writelines([timestamp, msg, "\n"])

    def error(self, msg: str):
        self._msg("ERR>>> " + msg)

    def err(self, msg: str):
        self.error(msg)

    def info(self, msg: str):
        self._msg("INFO>> " + msg)

    def debug(self, msg: str):
        if self._with_debug:
            self._msg("DEBUG>> " + msg)

    def trace(self, msg: str):
        if self._with_trace:
            self._msg("TRACE> " + msg)


# - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
class AlphonseConfig:

    def __init__(self, log: Logger, parser: ConfigParser):
        self._log = log
        self._parser = parser
        self._modem_device = None
        self._blacklist_file = None
        self._whitelist_file = None

    @property
    def modem_device(self): return self._modem_device
    @property
    def blacklist_file(self): return self._blacklist_file
    @property
    def whitelist_file(self): return self._whitelist_file

    def _config_check_modem_device(self, section: str, option: str) -> str:
        modem_device = self._parser.get(section, option)
        if modem_device is not None and len(modem_device) > 0:
            return modem_device

        raise Exception("no fall back for now - need some impl")
        # TODO try something around that
        # ports = []
        # for n, (port, desc, hwid) in enumerate(sorted(comports()), 1):
        #     sys.stderr.write('--- {:2}: {:20} {!r}\n'.format(n, port, desc))
        #     ports.append(port)

        return ""

    def _config_check_path_exists(self, section: str, option: str) -> str:
        file_path = self._parser.get(section, option)
        if file_path is None or len(file_path) == 0:
            raise Exception(f"Configuration: {section}#{option} was not defined")
        if not pathlib.Path(file_path).is_file():
            raise Exception(f"Configuration: file not found for {section}#{option} (was {file_path})")

        return file_path

    def load(self):
        self._modem_device = self._config_check_modem_device(CONF_SERVICE_SECTION_NAME, CONF_SERVICE_MODEM_DEVICE)
        self._blacklist_file = self._config_check_path_exists(CONF_SERVICE_SECTION_NAME, CONF_SERVICE_BLACKLIST_FILE)
        self._whitelist_file = self._config_check_path_exists(CONF_SERVICE_SECTION_NAME, CONF_SERVICE_WHITELIST_FILE)
        pass


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
class PhoneNumberHandlerContext:
    def __init__(self, number: str):
        self.number = number
        self.was_hangup = False
        self.should_stop_processing = False


# - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
class PhoneNumberFileExtractor:

    def __init__(self, file_path: str):
        self._file_path = file_path

    def get_numbers(self):
        result = []
        if not pathlib.Path(self._file_path).is_file():
            return result

        with open(self._file_path, 'r') as file:
            lines = file.readlines()

        for line in lines:
            line = line.translate({ord(i): None for i in ' _-.()[]\r\n'})
            if len(line) == 0:
                continue  # empty line
            if line.startswith("#"):
                continue  # commented line

            # add this number as is
            result.append(line)

            # try translate +33 xx... national phone number into local
            if line.startswith('+33'):
                result.append(line.replace("+33", "0", 1))

        return result


# - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
class PickUpHangUpBaseHandler:

    def __init__(self, modem: Modem, log: Logger):
        self._modem = modem
        self._log = log

    def process_unwanted_call(self, context: PhoneNumberHandlerContext, hangup_timeout: timedelta):
        # update context !!!
        self._log.trace("unwanted_call: pick-up call for " + str(hangup_timeout.total_seconds()) + " seconds")
        self._modem.write_command(MODEM_PICKUP)
        hangup_time = datetime.now() + hangup_timeout
        while datetime.now() < hangup_time:
            # Note : impossible to get state of hanging up by other side
            # TODO get case of analog line return
            self._modem.read(self._modem.in_waiting or 1)
            time.sleep(1)
        self._log.trace("unwanted_call: hang-up call for " + context.number)
        self._modem.write_command(MODEM_HANGUP)
        self._log.trace("unwanted_call: done ")
        context.was_hangup = True


# - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
class BlacklistHandler(PickUpHangUpBaseHandler):

    def __init__(self, modem: Modem, log: Logger, config: AlphonseConfig):
        super().__init__(modem, log)
        file_path = config.blacklist_file
        self._phone_number_extractor = PhoneNumberFileExtractor(file_path)

    def process(self, context: PhoneNumberHandlerContext):
        if context.was_hangup:
            return

        whitelist = self._phone_number_extractor.get_numbers()
        if context.number in whitelist:
            self.process_unwanted_call(context, timedelta(seconds=1))  # immediate

        return


# - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
class WhitelistHandler(PickUpHangUpBaseHandler):

    def __init__(self, modem: Modem, log: Logger, config: AlphonseConfig):
        super().__init__(modem, log)
        file_path = config.whitelist_file
        self._phone_number_extractor = PhoneNumberFileExtractor(file_path)

    def process(self, context: PhoneNumberHandlerContext):
        if context.was_hangup:
            return

        whitelist = self._phone_number_extractor.get_numbers()
        if context.number not in whitelist:
            self.process_unwanted_call(context, timedelta(seconds=13))

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
        # TODO history phone call handler here !!
        blacklist_handler = BlacklistHandler(modem, self._log, self._config)
        whitelist_handler = WhitelistHandler(modem, self._log, self._config)
        # TODO other handler I forget for now

        decoder = Decoder(self._log, blacklist_handler, whitelist_handler)
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
        log.err(f"Fail to read configuration point: {_err}")
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
