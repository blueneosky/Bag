import logging
import importlib
from abc import ABC, abstractmethod
from urllib.request import urlopen
from warnings import deprecated

import requests
from channels import Channel
from lxml import etree
from data_provider import Show

logger = logging.getLogger(__name__)


class Feeder(ABC):
    @staticmethod
    def create(channel: Channel) -> "Feeder":
        feeder_def = channel.feeder.split('.')
        module = importlib.import_module(f'feeders.{feeder_def[0]}')
        feeder_class = getattr(module, feeder_def[1])
        feeder: Feeder = feeder_class(channel)
        return feeder

    def __init__(self, channel: Channel) -> None:
        self.channel = channel
        pass

    @abstractmethod
    def get_shows_artifacts(self) -> list[Show]:
        pass

    @deprecated("change of phylosophy - will be removed in the future")
    @abstractmethod
    def get_data(self) -> str:
        pass

    def _get_xmlroot(self, url: str) -> tuple[etree._Element, dict]:
        logger.debug("Retriveing '%s' ...", url)
        with requests.get(url) as response:
            if not response.ok:
                raise response.raise_for_status()
            
            body = response.text
            xmlroot = etree.fromstring(body)
            nsmap = xmlroot.nsmap
            return (xmlroot, nsmap)

    def _get_json(self, url: str) -> dict:
        logger.debug("Retriveing '%s' ...", url)
        with requests.get(url) as response:
            if not response.ok:
                raise response.raise_for_status()
            return response.json()

    @deprecated("think about it")
    def update_channel(self, titles: list[str]) -> None:
        was_modified = False
        items = self.channel.items
        for title in titles:
            if title not in items:
                items[title] = True
                was_modified = True

        if was_modified:
            self.channel.save()

        pass
