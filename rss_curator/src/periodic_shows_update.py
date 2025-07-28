import logging
from datetime import timedelta
from channels import Channels
from data_service import DataService
from feeders.feeder import Feeder
from periodic_runner import PeriodicRunner

logger = logging.getLogger(__name__)


class PeriodicShowsUpdate(PeriodicRunner):
    def __init__(self, repeat_every: timedelta, after_initial_wait: timedelta | None = None):
        super().__init__(self.__loopEvent, repeat_every, after_initial_wait)

    def __loopEvent(self):
        self.update()
        pass

    @staticmethod
    def update():
        cfg = Channels.load()
        channels = [cfg.get_channel(channel["name"]) for channel in cfg.raw]
        for channel in channels:
            with DataService() as data_service:
                feeder = Feeder.create(channel)
                shows = feeder.get_shows_artifacts()
                data_service.update_shows(shows)
        pass
