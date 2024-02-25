from datetime import date, datetime, timezone
from channels import Channel
from feeders.feeder import Feeder
from lxml import etree
from email import utils
import requests


class Adn(Feeder):
    def __init__(self, channel: Channel) -> None:
        super().__init__(channel)
        pass

    def get_data(self) -> str:
        url = self.channel.url + f'&date={date.today().isoformat()}'
        json = requests.get(url).json()
        video_items: list(dict) = json["videos"]

        self.update_channel([item["show"]["title"] for item in video_items])

        items = self.channel.items

        def is_item_wanted(item: dict) -> bool:
            if (show_title := item["show"]["title"]) is None:
                return False
            if not items[show_title]:
                return False

            return True

        (xmlroot, _) = self.get_empty_rss_xmlroot()
        category: str = self.channel.raw["rssInfo"]["item.category"]
        channel_node: etree._Element = xmlroot.getchildren()[0]
        for src_item in filter(is_item_wanted, video_items):
            summary = src_item['summary'] or src_item["show"]["summary"] or "No description available"
            image = src_item['image']
            enclosure = self.get_imageinfo(image)
            pubdate = Adn.__get_rfc822_datetime(datetime.strptime(
                src_item['releaseDate'], "%Y-%m-%dT%H:%M:%SZ"))

            item_node: etree._Element = etree.Element("item")
            item_node.append(Adn.__create_element(
                "title", text=src_item["title"]))
            item_node.append(Adn.__create_element(
                "link", text=src_item["url"]))
            item_node.append(Adn.__create_element(
                "guid", text=src_item["embeddedUrl"], attributs={"isPermalink": "true"}))
            item_node.append(Adn.__create_element(
                "description", text=f"<img src=\"{image}\" /><br />{summary}"))
            if enclosure[0] is not None:
                channel_node.append(Adn.__create_element("enclosure", attributs={
                                    'url': enclosure[0], 'type': enclosure[1], 'length': enclosure[2]}))
            item_node.append(Adn.__create_element("category", text=category))
            item_node.append(Adn.__create_element("pubDate", text=pubdate))

            channel_node.append(item_node)
            pass

        result = etree.tostring(xmlroot, encoding='utf8')

        return result

    def get_empty_rss_xmlroot(self) -> (etree._Element, dict):
        raw = """<rss xmlns:media="http://search.yahoo.com/mrss/" version="2.0"><channel/></rss>"""
        xmlroot: etree._Element = etree.fromstring(raw)
        nsmap = xmlroot.nsmap

        rssinfo: dict = self.channel.raw["rssInfo"]

        channel_node: etree._Element = xmlroot.getchildren()[0]
        channel_node.append(Adn.__create_element(
            "title", text=rssinfo["title"]))
        channel_node.append(Adn.__create_element(
            "description", text=rssinfo["description"]))
        channel_node.append(Adn.__create_element("link", text=rssinfo["link"]))
        now = Adn.__get_rfc822_datetime()
        channel_node.append(Adn.__create_element("pubDate", text=now))
        channel_node.append(Adn.__create_element("lastBuildDate", text=now))

        return (xmlroot, nsmap)

    @staticmethod
    def __create_element(tag: str, text: str | None = None, attributs: dict[str, str] | None = None):
        el: etree._Element = etree.Element(tag)
        if text is not None:
            el.text = text
        if attributs is not None:
            for key, value in attributs.items():
                el.set(key, value)

        return el

    @staticmethod
    def __get_rfc822_datetime(value: datetime | None = None) -> str:
        value = value or datetime.now()
        return utils.format_datetime(value.astimezone(timezone.utc), usegmt=True)

    def get_imageinfo(self, url: str) -> (str, str, int):
        if "__imageinfo_cache__" in self.channel.raw:
            cache = self.channel.raw["__imageinfo_cache__"]
        else:
            cache = self.channel.raw["__imageinfo_cache__"] = dict()
        if url in cache:
            (_, ctype, clen) = cache[url]
            return (url, ctype, clen)
        
        (url, ctype, clen) = Adn.__get_imageinfo(url)
        if url is None:
            return (url, ctype, clen)
        
        date_now:date = datetime.now().date()
        self.channel.raw["__imageinfo_cache__"] = cache = { k:v for k,v in cache.items() if date.fromisoformat(v[0]) >= date_now }
        cache[url] = (date_now.isoformat(), ctype, clen)
        self.channel.save()
        return (url, ctype, clen)

    @staticmethod
    def __get_imageinfo(url: str) -> (str, str, int):
        try:
            """return (url, type, length)"""
            response: requests.Response = requests.get(url)
            if not response.ok:
                return (None, None, None)
            headers = response.headers
            content_type = headers["Content-Type"]
            content_length = headers["Content-Length"]
            if (content_type is None) or (content_length is None):
                return (None, None, None)

            return (url, content_type, content_length)
        except:
            return (None, None, None)
        pass
