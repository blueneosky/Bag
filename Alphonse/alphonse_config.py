import pathlib
from serial.tools.list_ports import comports
from configparser import ConfigParser

from alphonse_logger import Logger

CONFIG_PATH = "/etc/alphonse.conf"

CONF_SERVICE_SECTION_NAME = "SERVICE"
CONF_SERVICE_MODEM_DEVICE = "MODEM_DEVICE"
CONF_SERVICE_CALL_HISTORY_FILE = "CALL_HISTORY_FILE"
CONF_SERVICE_WHITELIST_FILE = "WHITELIST_FILE"
CONF_SERVICE_BLACKLIST_FILE = "BLACKLIST_FILE"
CONF_SERVICE_PHONE_BOOK_FILE = "PHONE_BOOK_FILE"
CONF_LOG_SECTION_NAME = "LOG"
CONF_LOG_FILE_NAME = "LOG_FILE"
CONF_LOG_WITH_DEBUG = "WITH_DEBUG"
CONF_LOG_WITH_TRACE = "WITH_TRACE"


# - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
class AlphonseConfig:

    def __init__(self, log: Logger, parser: ConfigParser):
        self._log = log
        self._parser = parser
        self._modem_device = None
        self._call_history_file = None
        self._blacklist_file = None
        self._whitelist_file = None
        self._phone_book_file = None

    @property
    def modem_device(self): return self._modem_device
    @property
    def call_history_file(self): return self._call_history_file
    @property
    def blacklist_file(self): return self._blacklist_file
    @property
    def whitelist_file(self): return self._whitelist_file
    @property
    def phone_book_file(self): return self._phone_book_file

    def _config_check_modem_device(self, section: str, option: str) -> str:
        modem_device = self._parser.get(section, option)
        if modem_device is not None and len(modem_device) > 0:
            return modem_device

        self._log.info(f"Option {option} was not defined - try do detect from current environment")
        devices = sorted(comports(), key=lambda comp: 'modem' not in comp[1].lower())
        if len(devices) == 0:
            self._log.error(" NO DEVICE FOUND !")
            exit(-1)

        for (port, desc, _) in devices:
            self._log.info(f' > found {port:20} [{desc}]')

        (modem_device, desc, _) = devices[0]
        self._log.info(f"Selected device : {modem_device} [{desc}]")

        return modem_device

    def _config_check_path_exists(self, section: str, option: str) -> str:
        file_path = self._parser.get(section, option)
        if file_path is None or len(file_path) == 0:
            raise Exception(f"Configuration: {section}#{option} was not defined")
        if not pathlib.Path(file_path).is_file():
            raise Exception(f"Configuration: file not found for {section}#{option} (was {file_path})")

        return file_path

    def load(self) -> "AlphonseConfig":
        self._modem_device = self._config_check_modem_device(CONF_SERVICE_SECTION_NAME, CONF_SERVICE_MODEM_DEVICE)
        self._call_history_file =\
            self._config_check_path_exists(CONF_SERVICE_SECTION_NAME, CONF_SERVICE_CALL_HISTORY_FILE)
        self._blacklist_file = self._config_check_path_exists(CONF_SERVICE_SECTION_NAME, CONF_SERVICE_BLACKLIST_FILE)
        self._whitelist_file = self._config_check_path_exists(CONF_SERVICE_SECTION_NAME, CONF_SERVICE_WHITELIST_FILE)
        self._phone_book_file = self._config_check_modem_device(CONF_SERVICE_SECTION_NAME, CONF_SERVICE_PHONE_BOOK_FILE)
        return self


