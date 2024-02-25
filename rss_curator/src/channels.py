import os.path
import json
from env import AppEnv


class Channel:
    def __init__(self, config: 'Channels', raw: dict) -> None:
        self.config = config
        self.raw = raw
        self.name = raw["name"]
        self.feeder = raw["__feeder__"]
        self.url = raw["url"]
        self.items: dict = raw["items"]

    def save(self) -> None:
        self.config.save()


class Channels:
    @staticmethod
    def load() -> 'Channels':
        # first/empty run
        if not os.path.exists(AppEnv.CHANNELS_FILE_PATH):
            with open(AppEnv.CHANNELS_FILE_PATH, 'w') as cfg_file:
                cfg_file.write("[]")

        with open(AppEnv.CHANNELS_FILE_PATH) as cfg_file:
            raw = json.load(cfg_file)

        return Channels(raw)

    def __init__(self, raw: dict) -> None:
        self.raw: dict = raw
        pass

    def save(self) -> None:
        with open(AppEnv.CHANNELS_FILE_PATH, 'w') as cfg_file:
            json.dump(self.raw, cfg_file, sort_keys=True,
                      indent=4, ensure_ascii=False)

    def get_channel(self, name: str) -> Channel:
        return next((Channel(self, c) for c in self.raw if c["name"] == name), None)
