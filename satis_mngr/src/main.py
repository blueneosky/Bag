#!/usr/bin/python3
import argparse
import uvicorn
from env import AppEnv
from webapi import app


def main():

    parser = argparse.ArgumentParser(description="Web Satisfactory Manager server.")
    parser.add_argument("--host", nargs='?', type=str,
                        default='127.0.0.1', help='Listened host. Default: %(default)s')
    parser.add_argument("--port", nargs='?', type=int,
                        default=6201, help='Listened port. Default: %(default)i')
    parser.add_argument("--servicename", nargs='?', type=str,
                        default=AppEnv.SERVICE_NAME, help='Service name. Default: "%(default)s"')
    args = parser.parse_args()

    AppEnv.SERVICE_NAME = args.servicename
    uvicorn.run(app, host=args.host, port=args.port)


if __name__ == "__main__":
    main()
