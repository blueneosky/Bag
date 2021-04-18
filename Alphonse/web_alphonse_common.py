import os
from configparser import ConfigParser

from alphonse_config import AlphonseConfig, CONFIG_PATH
from alphonse_logger import NullLogger


def get_html_header():
    return "Content-type: text/html\n\n"


def get_redirection(url: str):
    return f"{get_html_header()}<meta http-equiv=\"refresh\" content=\"0; url={url}\" />"


def is_local_call() -> bool:
    return "REMOTE_ADDR" in os.environ and os.environ["REMOTE_ADDR"].startswith("192.168.1.")


def get_alphonse_config() -> AlphonseConfig:
    config_parser = ConfigParser()
    config_parser.read_file(open(CONFIG_PATH))
    return AlphonseConfig(NullLogger(), config_parser)
