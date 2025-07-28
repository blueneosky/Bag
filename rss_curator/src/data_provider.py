from __future__ import annotations
from typing import List

from sqlalchemy import ForeignKey
from sqlalchemy import create_engine
from sqlalchemy.orm import Mapped, DeclarativeBase
from sqlalchemy.orm import relationship, mapped_column

from env import AppEnv

DataEngine = create_engine(f"sqlite:///{AppEnv.DB_FILE_PATH}",
                           connect_args={"check_same_thread": False},
                           echo=AppEnv.WITH_DB_LOGGING)


class Base(DeclarativeBase):
    pass


class Show(Base):
    __tablename__ = "shows"
    id: Mapped[int] = mapped_column(primary_key=True, index=True)
    feeder: Mapped[str] = mapped_column(index=True, nullable=False)
    title: Mapped[str] = mapped_column(index=True, nullable=False)
    thumbnail_url: Mapped[str] = mapped_column(nullable=True)

    # episodes: Mapped[List["Episode"]] = relationship(back_populates="show")
    episodes: Mapped[List["Episode"]] = relationship(back_populates=None)


class Episode(Base):
    __tablename__ = "episodes"
    id: Mapped[int] = mapped_column(primary_key=True, index=True)
    show_id: Mapped[int] = mapped_column(ForeignKey("shows.id"))
    guid: Mapped[str] = mapped_column(index=True, nullable=False)
    title: Mapped[str] = mapped_column(nullable=False)
    category: Mapped[str] = mapped_column(nullable=True)
    season: Mapped[int] = mapped_column(nullable=True)
    number: Mapped[int] = mapped_column(nullable=True)
    thumbnail_url: Mapped[str] = mapped_column(nullable=True)
    releaseDate: Mapped[str] = mapped_column(nullable=True)


class Watcher(Base):
    __tablename__ = "watchers"
    id: Mapped[int] = mapped_column(primary_key=True, index=True)
    username: Mapped[str] = mapped_column(nullable=False)

    followed_shows: Mapped[List["FollowedShow"]] = relationship()
    watched_episodes: Mapped[List["WatchedEpisode"]] = relationship()


class FollowedShow(Base):
    __tablename__ = "followed_shows"
    id: Mapped[int] = mapped_column(primary_key=True)
    watcher_id: Mapped[int] = mapped_column(ForeignKey("watchers.id"))
    show_id: Mapped[int] = mapped_column(ForeignKey("shows.id"))
    rating: Mapped[int] = mapped_column(nullable=False, default=0)

    show: Mapped["Show"] = relationship()


class WatchedEpisode(Base):
    __tablename__ = "watched_episodes"
    id: Mapped[int] = mapped_column(primary_key=True)
    watcher_id: Mapped[int] = mapped_column(ForeignKey("watchers.id"))
    episode_id: Mapped[int] = mapped_column(ForeignKey("episodes.id"))

    episode: Mapped["Episode"] = relationship()


Base.metadata.create_all(bind=DataEngine)
