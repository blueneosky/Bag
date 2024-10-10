from datetime import datetime, timedelta
import subprocess
import re

class Stopwatch:
    @staticmethod
    def startnew() -> 'Stopwatch':
        sw = Stopwatch()
        sw.start()
        return sw

    def __init__(self) -> None:
        self._start: datetime | None = None
        self._ellapsed: timedelta = timedelta()
        pass

    @property
    def ellapsed(self) -> timedelta:
        return self._ellapsed

    def start(self) -> None:
        if self._start is not None:
            return

        self._start = datetime.now()
        pass

    def stop(self) -> None:
        stop = datetime.now()
        if self._start is None:
            return

        diff = stop - self._start
        self._ellapsed += diff
        self._start = None
        pass

    def reset(self) -> None:
        self._start = None
        self._ellapsed = timedelta()
        pass

    def restart(self) -> None:
        self.reset()
        self.start()
        pass

    def __str__(self) -> str:
        if self._start is not None:
            self.stop()
            self.start()
        return str(self.ellapsed)

class SystemCtl:
    SUDO_CMD='/usr/bin/sudo'
    SYSTEMCTL_CMD='/usr/bin/systemctl'

    def __init__(self, service_name: str) -> None:
        self.service_name = service_name


    def get_status(self) -> str | None:
        return self.run('status', 5)
    
    def start(self) -> None:
        self.run('start', 1)
    
    def stop(self) -> None:
        self.run('stop', 1)
    
    def restart(self) -> None:
        self.run('restart', 1)
    

    def run(self, command: str, timout_sec: float | None) -> str | None:
        try:
            result = subprocess.run(
                [self.SUDO_CMD, self.SYSTEMCTL_CMD, command, self.service_name],
                capture_output=True,
                text=True,
                timeout=timout_sec
            )
            match = re.search("\\sActive:\\s*([^\\s]+)", result.stdout)
            raw_status = match.groups(0)[0]
            return raw_status
        except:
            return None