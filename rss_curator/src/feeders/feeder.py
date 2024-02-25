from abc import ABC, abstractmethod
from urllib.request import urlopen
from channels import Channel
from lxml import etree
import importlib


class Feeder(ABC):
    @staticmethod
    def create(channel: Channel) -> str:
        feeder_def = channel.feeder.split('.')
        module = importlib.import_module(f'feeders.{feeder_def[0]}')
        feeder_class = getattr(module, feeder_def[1])
        feeder: Feeder = feeder_class(channel)
        return feeder

    def __init__(self, channel: Channel) -> None:
        self.channel = channel
        pass

    @abstractmethod
    def get_data(self) -> str:
        pass

    def get_xmlroot(self, url: str) -> (etree._Element, dict):
        raw = urlopen(url).read()
        xmlroot = etree.fromstring(raw.decode('utf-8'))
        nsmap = xmlroot.nsmap
        return (xmlroot, nsmap)

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
