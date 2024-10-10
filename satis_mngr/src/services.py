from enum import Enum

from env import AppEnv
from utils import SystemCtl

class ServiceStatus(Enum):
    UNKNOWN = 0
    STOPPED = 1
    STARTED = 2
    STARTING = 3


class Service:
    def __init__(self, name: str) -> None:
        self.name = name

    def get_status(self) -> ServiceStatus:
        raw_status = SystemCtl(self.name).get_status()
        match raw_status:
            case 'inactive':
                return ServiceStatus.STOPPED
            case 'active':
                return ServiceStatus.STARTED
            case 'activating':
                return ServiceStatus.STARTING
            case 'deactivating':
                return ServiceStatus.STARTING
            case _:
                return ServiceStatus.UNKNOWN
    
    def start(self) -> None:
        SystemCtl(self.name).start()

    def stop(self) -> None:
        SystemCtl(self.name).stop()

    def restart(self) -> None:
        SystemCtl(self.name).restart()


class Services:
    def __init__(self) -> None:
        self.service_names = [AppEnv.SERVICE_NAME]

    def get_services(self) -> list[Service]:
        return (Service(n) for n in self.service_names)
    
    def get_service(self, name: str) -> Service | None:
        return next((Service(n) for n in self.service_names if n == name), None)