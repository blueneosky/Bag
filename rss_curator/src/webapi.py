from fastapi import FastAPI, HTTPException, Response
from fastapi.responses import FileResponse, RedirectResponse, HTMLResponse
from fastapi.staticfiles import StaticFiles
from channels import Channel, Channels
from feeders.feeder import Feeder
from urllib import parse

app = FastAPI()
app.mount("/static", StaticFiles(directory="static", html=True), name="static")


@app.get("/", response_class=RedirectResponse)
async def get_root():
    return RedirectResponse("./static/index.html")


@app.get("/channels")
async def get_channels():
    cfg = Channels.load()
    return __to_channels_dto(cfg)


@app.get("/channels/{channel_id}")
async def get_channel(channel_id: str):
    channelname = parse.unquote_plus(channel_id)
    cfg = Channels.load()
    channel = cfg.get_channel(channelname)
    if channel is None:
        raise HTTPException(
            status_code=404, detail=f"Unknown channel id '{channel_id}'")
    return __to_channel_dto(channel, withitems=True)


@app.patch("/channels/{channel_id}/items/{title_id}")
async def patch_channel_item(channel_id: str, title_id: str, followed: int):
    channelname = parse.unquote_plus(channel_id)
    titlename = parse.unquote_plus(title_id)
    cfg = Channels.load()
    channel = cfg.get_channel(channelname)
    if channel is None:
        raise HTTPException(
            status_code=404, detail=f"Unknown channel id '{channel_id}'")
    if titlename not in channel.items:
        raise HTTPException(
            status_code=404, detail=f"Unknown title id '{title_id}'")
    channel.items[titlename] = followed != 0
    channel.save()
    return __to_channel_dto(channel, True)


@app.get("/{channel_id}.xml")
async def get_rss(channel_id: str):
    channelname = parse.unquote_plus(channel_id)
    cfg = Channels.load()
    channel = cfg.get_channel(channelname)
    if channel is None:
        return Response(content=f"<error>Unknown channel '{channelname}'</error>", status_code=404, media_type="application/xml")
    feeder = Feeder.create(channel)
    raw = feeder.get_data()
    return Response(content=raw, media_type="application/xml")


def __to_channels_dto(channels: Channels, withitems: bool | None = None):
    dto = [__to_channel_dto(channels.get_channel(channel["name"]), withitems)
           for channel in channels.raw]
    return dto


def __to_channel_dto(channel: Channel, withitems: bool | None = None):
    dto = {'id': parse.quote_plus(channel.name),
           'title': channel.name,
           'relativeUrl': f'{channel.name}.xml'}
    if withitems:
        dto['items'] = [{"id": parse.quote_plus(title),
                         "title": title,
                         "followed": followed
                         } for title, followed in channel.items.items()]

    return dto
