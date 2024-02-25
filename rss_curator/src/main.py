#!/usr/bin/python3
import argparse
import uvicorn
from env import AppEnv
from webapi import app


def main():

    parser = argparse.ArgumentParser(description="Web Rss curator server.")
    parser.add_argument("--host", nargs='?', type=str,
                        default='127.0.0.1', help='Listened host. Default: %(default)s')
    parser.add_argument("--port", nargs='?', type=int,
                        default=6110, help='Listened port. Default: %(default)i')
    parser.add_argument("--channels", nargs='?', type=str,
                        default=AppEnv.CHANNELS_FILE_PATH, help='Channels file path with preferences. Default: "%(default)s"')
    args = parser.parse_args()

    AppEnv.CHANNELS_FILE_PATH = args.channels
    uvicorn.run(app, host=args.host, port=args.port)


if __name__ == "__main__":
    main()
