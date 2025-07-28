import logging
from typing import Generator
from sqlalchemy.orm import sessionmaker, Session
from data_provider import DataEngine, Episode, FollowedShow, Show, Watcher

logger = logging.getLogger(__name__)

def create_data_service() -> Generator["DataService", None, None]:
    with DataService() as data_service:
        yield data_service


class DataService:
    def __init__(self):
        self.db_session: Session = sessionmaker(
            autocommit=False, autoflush=False, bind=DataEngine)()

    def __enter__(self):
        self.db_session.__enter__()
        return self

    def __exit__(self, exc_type, exc_val, exc_tb):
        self.db_session.__exit__(exc_type, exc_val, exc_tb)
        pass

    def init_db(self) -> None:
        logger.debug("Checking Anonymous watcher...")
        db = self.db_session
        anonymous = db.query(Watcher).filter(
            Watcher.username == "anonymous").first()
        if not anonymous:
            anonymous = Watcher(username="anonymous")
            db.add(anonymous)
            db.commit()
            db.refresh(anonymous)
            logger.info("Watcher '%s' added (#%s)",
                        anonymous.username, anonymous.id)

    def update_shows(self, shows: list[Show]) -> None:
        watchers = self.db_session.query(Watcher).all()
        assert len(watchers) >= 1, "There should be at least one watcher (anonymous)"

        for s in shows:
            self.update_show(s, watchers)

    def update_show(self, show: Show, watchers: list[Watcher]) -> None:
        db = self.db_session
        
        # Avoid relationnal insertions
        episodes = show.episodes.copy()
        show.episodes.clear()
        
        existing_show = db.query(Show).filter(
            Show.title == show.title,
            Show.feeder == show.feeder
        ).first()
        if existing_show:
            show = existing_show
        else:
            db.add(show)
            db.commit()
            db.refresh(show)

            for watcher in watchers:
                watcher.followed_shows.append(FollowedShow(show=show))

        self.update_episodes(show, episodes)

    def update_episodes(self, show: Show, episodes: list[Episode]) -> None:
        db = self.db_session

        for episode in episodes:
            existing_episode_id = db.query(Episode.id).filter(
                Episode.guid == episode.guid,
                Episode.show_id == show.id
            ).first()
            if existing_episode_id is None:
                show.episodes.append(episode)
        db.commit()
        
