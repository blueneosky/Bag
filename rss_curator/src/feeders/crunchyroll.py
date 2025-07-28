import logging
import re
from datetime import datetime, timezone
from warnings import deprecated
from xml.etree.ElementTree import Element
from channels import Channel
from data_provider import Episode, Show
from feeders.feeder import Feeder
from lxml import etree
from py_linq import Enumerable
from utils import try_parse_int
from dateutil import parser

logger = logging.getLogger(__name__)


class Crunchyroll(Feeder):
    __DUB_PATTERN = re.compile(r"\([^)]+? [dD]ub\)")

    def __init__(self, channel: Channel) -> None:
        super().__init__(channel)
        pass

    def get_shows_artifacts(self) -> list[Show]:
        (xmlroot, nsmap) = self._get_xmlroot(self.channel.url)

        items: list[Element] = xmlroot.xpath(
            "/rss/channel/item", namespaces=nsmap)

        serie_by_titles: dict[str, Show] = {}

        for item in items:
            try:
                self.__process_item(serie_by_titles, item, nsmap)

            except Exception as e:
                logger.exception("Error processing item")
                continue

        return list(serie_by_titles.values())

    def __process_item(self, serie_by_titles: dict[str, Show], item: Element, nsmap: dict) -> None:
        if not self.__check_country_restriction(item, nsmap):
            return

        serie_title = item.findtext(
            "crunchyroll:seriesTitle", namespaces=nsmap)
        serie = serie_by_titles.setdefault(serie_title, Show(
            title=serie_title,
            feeder=self.channel.name,
            thumbnail_url=None,
            episodes=[]))

        thumbnail_url = Enumerable(item.findall("media:thumbnail", namespaces=nsmap)) \
            .select(lambda thumb: thumb.get("url")) \
            .where(lambda thumb: thumb is not None) \
            .order_by(lambda thumb: 0 if thumb.endswith("thumb.jpg") else 1) \
            .first_or_default()

        release_date = item.findtext("pubDate", namespaces=nsmap)
        release_date = parser.parse(release_date) if release_date is not None \
            else datetime.now(timezone.utc)

        episode = Episode(
            guid=item.findtext("guid", namespaces=nsmap),
            title=item.findtext("crunchyroll:episodeTitle", namespaces=nsmap),
            category=item.findtext("category", namespaces=nsmap),
            season=try_parse_int(item.findtext(
                "crunchyroll:season", namespaces=nsmap)),
            number=try_parse_int(item.findtext(
                "crunchyroll:episodeNumber", namespaces=nsmap)),
            thumbnail_url=thumbnail_url,
            releaseDate=release_date.isoformat(),
        )
        serie.episodes.append(episode)

    def __check_country_restriction(self, item: Element, nsmap: dict) -> bool:
        if (restriction := item.find("media:restriction", namespaces=nsmap)) is not None \
                and restriction.get("type") == "country":

            if restriction.get("relationship") == "allow" and restriction.text.find("fr") == -1:
                return False

            if restriction.get("relationship") == "deny" and restriction.text.find("fr") >= -1:
                return False

        if (subtitleLanguages := item.findtext("crunchyroll:subtitleLanguages", namespaces=nsmap)) is None \
                or subtitleLanguages.find("fr") == -1:

            return False

        return True

    # ==OLD impl ================================================================
    @deprecated("remove this oeverride in the future")
    def get_data(self) -> str:
        logger.debug(">>> self.__channel: %s", self.channel)
        (xmlroot, nsmap) = self._get_xmlroot(self.channel.url)

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
