import logging
from datetime import timedelta
from contextlib import asynccontextmanager
from fastapi import Depends, FastAPI, HTTPException, Response
from fastapi.responses import RedirectResponse
from fastapi.staticfiles import StaticFiles
from channels import Channel, Channels
from data_provider import Episode, FollowedShow, Show, WatchedEpisode, Watcher
from data_service import DataService, create_data_service
from env import AppEnv
from feeders.feeder import Feeder
from periodic_shows_update import PeriodicShowsUpdate
from urllib import parse


logging.basicConfig(
    level=logging.DEBUG if AppEnv.IS_DEBUG else logging.INFO,
    format=">%(levelname)-8s [%(name)s] %(message)s",
)

logger = logging.getLogger(__name__)


@asynccontextmanager
async def lifespan(app: FastAPI):
    periodic_puller_task = None
    try:
        # startup things...
        with DataService() as data_service:
            data_service.init_db()
        periodic_puller = PeriodicShowsUpdate(
            repeat_every=timedelta(minutes=20),
            after_initial_wait=timedelta(seconds=20))
        periodic_puller_task = periodic_puller.run()
        yield
    finally:
        # shuting down things...
        periodic_puller_task.cancel() if periodic_puller_task is not None else None

        pass
    pass

logger.info("Current Environment: %s", AppEnv.ENV)

if AppEnv.IS_PROD:
    app = FastAPI(lifespan=lifespan, openapi_url=None,
                  docs_url=None, redoc_url=None)
else:
    app = FastAPI(lifespan=lifespan)

app.mount("/static", StaticFiles(directory="static", html=True), name="static")


@app.get("/", response_class=RedirectResponse)
async def get_root():
    return RedirectResponse("./static/index.html")


@app.get("/health")
async def health_check():
    return {"status": "ok"}


@app.post("/shows/update")
async def post_shows_update():
    PeriodicShowsUpdate.update()
    return {"status": "ok"}


@app.get("/test")
async def get_test(data_service: DataService = Depends(create_data_service)):
    watcher = data_service.db_session.query(Watcher).filter(
        Watcher.username == "anonymous").first()

    return __tmp_watcher_to_dto(watcher)


def __tmp_watcher_to_dto(watcher: Watcher):
    return {
        "id": watcher.id,
        "username": watcher.username,
        "followed_shows": [__tmp_followed_show_to_dto(fs) for fs in watcher.followed_shows],
        "watched_episodes": [__tmp_watched_episode_to_dto(we) for we in watcher.watched_episodes]
    }


def __tmp_followed_show_to_dto(followed_show: FollowedShow):
    return {
        # "id": followed_show.id,
        "rating": followed_show.rating,
        "show": __tmp_show_to_dto(followed_show.show)
    }


def __tmp_watched_episode_to_dto(watched_episode: WatchedEpisode):
    return __tmp_episode_to_dto(watched_episode.episode)


def __tmp_show_to_dto(show: Show):
    return {
        "id": show.id,
        "title": show.title,
        "feeder": show.feeder,
        "thumbnail_url": show.thumbnail_url,
        "episodes": [__tmp_episode_to_dto(ep) for ep in show.episodes]
    }


def __tmp_episode_to_dto(episode: Episode):
    return {
        "id": episode.id,
        "guid": episode.guid,
        "title": episode.title,
        "category": episode.category,
        "season": episode.season,
        "number": episode.number,
        "thumbnail_url": episode.thumbnail_url,
        "releaseDate": episode.releaseDate,
    }

# === OLD API =================================================


@app.get("/channels")
async def get_channels():
    cfg = Channels.load()
    return __to_channels_dto(cfg)


@app.get("/channels/{channel_id}")
async def get_channel(channel_id: str):
    channelname = parse.unquote_plus(channel_id)
    cfg = Channels.load()
    channel = cfg.get_channel(channelname)
    if channel is None:
        raise HTTPException(
            status_code=404, detail=f"Unknown channel id '{channel_id}'")
    return __to_channel_dto(channel, withitems=True)


@app.patch("/channels/{channel_id}/items/{title_id}")
async def patch_channel_item(channel_id: str, title_id: str, followed: int):
    channelname = parse.unquote_plus(channel_id)
    titlename = parse.unquote_plus(title_id)
    cfg = Channels.load()
    channel = cfg.get_channel(channelname)
    if channel is None:
        raise HTTPException(
            status_code=404, detail=f"Unknown channel id '{channel_id}'")
    if titlename not in channel.items:
        raise HTTPException(
            status_code=404, detail=f"Unknown title id '{title_id}'")
    channel.items[titlename] = followed != 0
    channel.save()
    return __to_channel_dto(channel, True)


@app.get("/{channel_id}.xml")
async def get_rss(channel_id: str):
    channelname = parse.unquote_plus(channel_id)
    cfg = Channels.load()
    channel = cfg.get_channel(channelname)
    if channel is None:
        return Response(content=f"<error>Unknown channel '{channelname}'</error>", status_code=404, media_type="application/xml")
    feeder = Feeder.create(channel)
    raw = feeder.get_data()
    return Response(content=raw, media_type="application/xml")


def __to_channels_dto(channels: Channels, withitems: bool | None = None):
    dto = [__to_channel_dto(channels.get_channel(channel["name"]), withitems)
           for channel in channels.raw]
    return dto


def __to_channel_dto(channel: Channel, withitems: bool | None = None):
    dto = {'id': parse.quote_plus(channel.name),
           'title': channel.name,
           'relativeUrl': f'{channel.name}.xml'}
    if withitems:
        dto['items'] = [{"id": parse.quote_plus(title),
                         "title": title,
                         "followed": followed
                         } for title, followed in channel.items.items()]

    return dto
