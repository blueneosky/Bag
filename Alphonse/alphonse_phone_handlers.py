import json
import time
from datetime import datetime, timedelta

from alphonse_logger import Logger
from alphonse_config import AlphonseConfig
from alphonse_modem import Modem, MODEM_PICKUP, MODEM_HANGUP
from alphonse_phonenumbers import PhoneNumberFileExtractor


# - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
class PhoneNumberHandlerContext:
    def __init__(self, number: str):
        self.number = number
        self.was_hangup = False
        self.should_stop_processing = False


# - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
class CallHistoryHandler:

    def __init__(self, log: Logger, conf: AlphonseConfig):
        self._log = log
        self._conf = conf

    def process(self, context: PhoneNumberHandlerContext):
        history = {'timestamp': datetime.now().isoformat(), 'number': context.number}
        serial_history = json.dumps(history, ensure_ascii=False)
        with open(self._conf.call_history_file, "a") as logfile:
            logfile.writelines([serial_history, '\n'])
        self._log.trace(f"Call history updated")

        return


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


