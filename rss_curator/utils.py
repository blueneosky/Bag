from datetime import datetime, timedelta


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
