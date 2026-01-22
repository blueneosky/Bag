import asyncio
import logging
from datetime import timedelta
from typing import Awaitable, Callable

logger = logging.getLogger(__name__)


class PeriodicRunner:
    def __init__(self, repeatedAction: callable | Callable[..., Awaitable],
                 repeatEvery: timedelta,
                 afterInitialWait: timedelta | None = None):
        self.__repeatedAction = repeatedAction
        self.__repeatEvery = repeatEvery
        self.__afterInitialWait = afterInitialWait

    def run(self) -> asyncio.Task:
        return asyncio.create_task(self.__loop())

    async def __loop(self):
        if self.__afterInitialWait:
            await asyncio.sleep(self.__afterInitialWait.total_seconds())

        while True:
            try:
                if asyncio.iscoroutinefunction(self.__repeatedAction):
                    await self.__repeatedAction()
                else:
                    self.__repeatedAction()
            except Exception:
                logger.exception(f"Unexpected error")

            await asyncio.sleep(self.__repeatEvery.total_seconds())
