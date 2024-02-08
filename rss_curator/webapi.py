from fastapi import FastAPI, HTTPException, Response
from fastapi.responses import FileResponse, RedirectResponse, HTMLResponse
from assets import Assets, IndexPage
from channels import Channels
from feeders.feeder import Feeder
import re
from urllib import parse

__ASSETS_VALIDATION_PATTERN = re.compile(r"^(\.?[\w\d_]+)+$")

app = FastAPI()


@app.get("/", response_class=RedirectResponse)
async def get_root():
    return RedirectResponse("./index.html")


@app.get("/index.html", response_class=HTMLResponse)
async def get_index():
    return HTMLResponse(IndexPage.get_content())


@app.get("/assets/{filename}", response_class=FileResponse)
async def get_assets(filename: str):
    return Assets.get_assets_response(filename)


@app.get("/channels")
async def get_channels():
    cfg = Channels.load()
    names = [channel["name"] for channel in cfg.raw]
    channels = [{'id': parse.quote_plus(name),
                 'title': name,
                 'relativeUrl': f'{name}.xml'
                 } for name in names]
    return channels


@app.get("/channels/{channel_id}")
async def get_channels(channel_id: str):
    channelname = parse.unquote_plus(channel_id)
    cfg = Channels.load()
    channel = cfg.get_channel(channelname)
    if channel is None:
        return Response(content=f"<error>Unknown channel '{channelname}'</error>", status_code=404, media_type="application/xml")
    titles = [{"id": parse.quote_plus(title),
               "title": title,
               "followed": followed
               } for title, followed in channel.items.items()]
    return titles


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
