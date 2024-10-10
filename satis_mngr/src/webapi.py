from fastapi import FastAPI, HTTPException, Response
from fastapi.responses import FileResponse, RedirectResponse, HTMLResponse
from fastapi.staticfiles import StaticFiles
from urllib import parse

from services import Service, Services

app = FastAPI()
app.mount("/static", StaticFiles(directory="static", html=True), name="static")


@app.get("/", response_class=RedirectResponse)
async def get_root():
    return RedirectResponse("./static/index.html")


@app.get("/services")
async def get_services():
    return __to_services_dto(Services())


@app.get("/services/{service_id}")
async def get_service(service_id: str):
    service_name = parse.unquote_plus(service_id)
    service = Services().get_service(service_name)
    if service is None:
        raise HTTPException(
            status_code=404, detail=f"Unknown service id '{service_id}'")
    return __to_service_dto(service)


@app.post("/services/{service_id}/{command}")
async def patch_channel_item(service_id: str, command: str):
    service_name = parse.unquote_plus(service_id)
    service = Services().get_service(service_name)
    if service is None:
        raise HTTPException(
            status_code=404, detail=f"Unknown service id '{service_id}'")
    if command not in ['start', 'stop', 'restart']:
        raise HTTPException(
            status_code=400, detail=f"Unknown command '{command}'")
    getattr(service, command)()
    return __to_service_dto(service)


def __to_services_dto(services: Services | list[Service]) -> list[dict]:
    if isinstance(services, Services):
        services = services.get_services()
    dto = [__to_service_dto(s) for s in services]
    return dto

def __to_service_dto(service: Service) -> dict:
    dto = {'id': parse.quote_plus(service.name),
           'name': service.name,
           'status': service.get_status().name}
    return dto