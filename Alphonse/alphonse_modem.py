import serial

from typing import List


# don't forget, for voice mode https://en.wikipedia.org/wiki/Voice_modem_command_set
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
