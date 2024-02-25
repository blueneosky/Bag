import re
from channels import Channel
from feeders.feeder import Feeder
from lxml import etree


class Crunchyroll(Feeder):
    __DUB_PATTERN = re.compile(r"\([^)]+? [dD]ub\)")

    def __init__(self, channel: Channel) -> None:
        super().__init__(channel)
        pass

    def get_data(self) -> str:
        (xmlroot, nsmap) = self.get_xmlroot(self.channel.url)

        self.update_channel([str(node) for node in xmlroot.xpath(
            "/rss/channel/item/crunchyroll:seriesTitle/text()", namespaces=nsmap)])

        items = self.channel.items

        def is_item_unwanted(item):
            if (seriestitle_item := item.find("crunchyroll:seriesTitle", namespaces=nsmap)) is None:
                return True
            seriestitle = seriestitle_item.text
            if not items[seriestitle]:
                return True

            if (title_item := item.find("title")) is None:
                return True
            if not (title := title_item.text):
                return True

            if Crunchyroll.__DUB_PATTERN.search(title, len(seriestitle)) is not None:
                return True

            return False

        for item in filter(is_item_unwanted, xmlroot.xpath("/rss/channel/item", namespaces=nsmap)):
            item.getparent().remove(item)

        return etree.tostring(xmlroot, encoding='utf8')
