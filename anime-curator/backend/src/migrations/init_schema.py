from sqlalchemy import Connection, text
from migrations.migration import Migration

class InitSchema(Migration):
    def __init__(self, conn: Connection) -> None:
        super().__init__(conn)

    def upgrade(self):
        self.execute("""CREATE TABLE IF NOT EXISTS watchers (
            id INTEGER NOT NULL, 
            username VARCHAR NOT NULL, 
            PRIMARY KEY (id)
        )""")

        self.execute("""CREATE TABLE IF NOT EXISTS shows (
            id INTEGER NOT NULL, 
            feeder VARCHAR NOT NULL, 
            title VARCHAR NOT NULL, 
            thumbnail_url VARCHAR, 
            PRIMARY KEY (id)
        )""")
        self.execute("""CREATE TABLE IF NOT EXISTS followed_shows (
            id INTEGER NOT NULL, 
            watcher_id INTEGER NOT NULL, 
            show_id INTEGER NOT NULL, 
            rating INTEGER NOT NULL, 
            PRIMARY KEY (id), 
            FOREIGN KEY(watcher_id) REFERENCES watchers (id), 
            FOREIGN KEY(show_id) REFERENCES shows (id)
        )""")
        self.execute("""CREATE TABLE IF NOT EXISTS episodes (
            id INTEGER NOT NULL, 
            show_id INTEGER NOT NULL, 
            guid VARCHAR NOT NULL, 
            title VARCHAR NOT NULL, 
            category VARCHAR, 
            season INTEGER, 
            number INTEGER, 
            thumbnail_url VARCHAR, 
            "releaseDate" VARCHAR, 
            PRIMARY KEY (id), 
            FOREIGN KEY(show_id) REFERENCES shows (id)
        )""")
        self.execute("""CREATE TABLE IF NOT EXISTS watched_episodes (
            id INTEGER NOT NULL, 
            watcher_id INTEGER NOT NULL, 
            episode_id INTEGER NOT NULL, 
            PRIMARY KEY (id), 
            FOREIGN KEY(watcher_id) REFERENCES watchers (id), 
            FOREIGN KEY(episode_id) REFERENCES episodes (id)
        )""")
       
        self.execute("""CREATE INDEX ix_episodes_guid ON episodes (guid)""")
        self.execute("""CREATE INDEX ix_episodes_id ON episodes (id)""")
        self.execute("""CREATE INDEX ix_shows_feeder ON shows (feeder)""")
        self.execute("""CREATE INDEX ix_shows_id ON shows (id)""")
        self.execute("""CREATE INDEX ix_shows_title ON shows (title)""")
        self.execute("""CREATE INDEX ix_watchers_id ON watchers (id)""")

        pass