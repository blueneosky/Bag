import logging
from datetime import timedelta
from contextlib import asynccontextmanager
from fastapi import Depends, FastAPI, HTTPException, Response
from fastapi.responses import RedirectResponse
from fastapi.staticfiles import StaticFiles
from channels import Channel, Channels
from data_provider import DataEngine, Episode, FollowedShow, Show, WatchedEpisode, Watcher
from data_service import DataService, create_data_service
from env import AppEnv
from feeders.feeder import Feeder
from migrations.migration_manager import MigrationManager
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

apiInfo = ("Anime Curator API", "Backend API for Anime Curator", "0.1.0")
if AppEnv.IS_PROD:
    app = FastAPI(lifespan=lifespan, title=apiInfo[0], description=apiInfo[1],
                  version=apiInfo[2], openapi_url=None, docs_url=None, redoc_url=None)

    app.mount("/static", StaticFiles(directory="static", html=True), name="static")
else:
    app = FastAPI(lifespan=lifespan,
                  title=apiInfo[0], description=apiInfo[1], version=apiInfo[2])


@app.get("/", response_class=RedirectResponse)
async def get_root():
    return RedirectResponse("./static/index.html")


@app.get("/health")
async def health_check():
    return {"status": "ok"}


@app.get("/test")
async def get_test(data_service: DataService = Depends(create_data_service)):
    with DataEngine.begin() as conn:
        schema_version = MigrationManager(conn).get_current_version()

    return {"schema_version": schema_version}


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


@app.get("/api/shows")
async def get_shows(limit: int = 50, data_service: DataService = Depends(create_data_service)):
    shows = data_service.db_session.query(Show).limit(limit).all()
    return [__tmp_show_to_dto(show) for show in shows]


@app.get("/api/shows/{show_id}")
async def get_show(show_id: int, data_service: DataService = Depends(create_data_service)):
    show = data_service.db_session.query(
        Show).filter(Show.id == show_id).first()
    if show is None:
        raise HTTPException(status_code=404, detail="Show not found")
    return __tmp_show_to_dto(show)


@app.post("/api/shows/update")
async def post_shows_update():
    PeriodicShowsUpdate.update()
    return {"status": "ok"}
