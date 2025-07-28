import os


class AppEnv:
    ENV: str = os.getenv("ENV", "dev")
    CHANNELS_FILE_PATH: str = "./channels.json"
    DB_FILE_PATH: str = "/data/rss-curator.db"

    IS_PROD: bool = ENV == "prod"
    IS_DEBUG: bool = ENV == "dev"

    WITH_DB_LOGGING: bool = False#IS_DEBUG
