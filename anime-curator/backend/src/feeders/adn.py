import logging
from datetime import date, datetime, timezone
from channels import Channel
from data_provider import Episode, Show
from feeders.feeder import Feeder
from py_linq import Enumerable
from dateutil import parser
from utils import try_parse_int

logger = logging.getLogger(__name__)


class Adn(Feeder):
    def __init__(self, channel: Channel) -> None:
        super().__init__(channel)
        pass

    def get_shows_artifacts(self) -> list[Show]:
        url = self.channel.url + f'&date={date.today().isoformat()}'
        json = self._get_json(url)
        items: list[dict] = json["videos"]

        serie_by_titles: dict[str, Show] = {}

        for item in items:
            try:
                self.__process_item(serie_by_titles, item)

            except Exception as e:
                logger.exception("Error processing item")
                continue

        return list(serie_by_titles.values())

    def __process_item(self, serie_by_titles: dict[str, Show], video: dict) -> None:
        if not self.__check_country_restriction(video):
            return

        show = video["show"]
        serie_title = show["title"]
        serie = serie_by_titles.setdefault(serie_title, Show(
            title=serie_title,
            feeder=self.channel.name,
            thumbnail_url=show['image'],
            episodes=[]))

        release_date = video['releaseDate']
        release_date = parser.parse(release_date) if release_date is not None \
            else datetime.now(timezone.utc)

        episode = Episode(
            guid=video['embeddedUrl'],
            title=video['name'],
            category=Enumerable(show['genres']).first_or_default(),
            season=try_parse_int(video['season']),
            number=try_parse_int(video['shortNumber']),
            thumbnail_url=video['image'],
            releaseDate=release_date.isoformat(),
        )
        serie.episodes.append(episode)

    def __check_country_restriction(self, item: dict) -> bool:
        if item["show"]["title"] is None:
            return False
        if (languages := item['languages']) is not None:
            if "fr" not in languages and "vostf" not in languages:
                return False

        return True
