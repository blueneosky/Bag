from datetime import datetime


# - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
class Logger:
    def __init__(self, file_path: str = None, with_debug: bool = True, with_trace: bool = True):
        self._file_path = file_path
        self._with_debug = with_debug
        self._with_trace = with_trace
        self._file_logging_failed=False

    def _msg(self, msg: str):
        print(msg)
        timestamp = datetime.now().strftime("%Y/%m/%d %H:%M:%S ")
        if self._file_path is None:
            return
        if not self._file_logging_failed:
            try:
                with open(self._file_path, "a") as logfile:
                    logfile.writelines([timestamp, msg, "\n"])
            except PermissionError:
                self._file_logging_failed = True

        return

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
class NullLogger(Logger):
    def __init__(self):
        super(NullLogger, self).__init__()

    def _msg(self, msg: str):
        pass
