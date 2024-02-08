import base64
from fastapi import Response
from fastapi.responses import HTMLResponse
from channels import Channels


class IndexPage:
    def get_content() -> str:
        config = Channels.load()

        return Assets.get_index_html_content()


class Assets:
    @classmethod
    def get_index_html_content(cls) -> str:
        # TODO - tranform or delete that
        return cls.__get_filecontent('index.html')

    @classmethod
    def get_assets_response(cls, filename: str) -> Response:
        match filename:
            case 'styles.css':
                return cls.__get_styles_css_response()
            case 'main.js':
                return cls.__get_main_js_response()
            case 'rss-icon.svg':
                return cls.__get_cached_assets_response(cls.__RSS_ICON_SVG)
                return Response(content=cls.__RSS_ICON_SVG_CONTENT, media_type=cls.__CTYPE_SVG_XML, headers={'Cache-Control': 'max-age=3600'})
            case 'settings-icon.svg':
                return cls.__get_cached_assets_response(cls.__SETTINGS_ICON_SVG)
                return Response(content=cls.__SETTINGS_ICON_SVG_CONTENT, media_type=cls.__CTYPE_SVG_XML, headers={'Cache-Control': 'max-age=3600'})
            case 'goback-icon.svg':
                return cls.__get_cached_assets_response(cls.__GOBACK_ICON_SVG)
                return Response(content=cls.__GOBACK_ICON_SVG_CONTENT, media_type=cls.__CTYPE_SVG_XML, headers={'Cache-Control': 'max-age=3600'})
            case _:
                return HTMLResponse(content="<h1>404 - Not found</h1>", status_code=404)

    @classmethod
    def __get_filepath(cls, filename: str) -> str:
        return f'./assets/{filename}'

    @classmethod
    def __get_filecontent(cls, filename: str) -> str:
        filepath = cls.__get_filepath(filename)
        with open(filepath) as file:
            return file.read()

    @classmethod
    def __get_cached_assets_response(cls, resx: dict):
        return Response(content=resx['content'],
                        media_type=resx['content_type'],
                        headers={'Cache-Control': 'max-age=3600'})

    @classmethod
    def __get_styles_css_response(cls) -> Response:
        # TODO - inline this file !!
        content = cls.__get_filecontent('styles.css')
        return Response(content, media_type=cls.__CTYPE_TEXT_CSS_UTF8)

    @classmethod
    def __get_main_js_response(cls) -> Response:
        # TODO - inline this file !!
        content = cls.__get_filecontent('main.js')
        return Response(content, media_type=cls.__CTYPE_APP_JS)

    """
    ========================
     ALL INLINED RESOURCES
    ========================
    """

    __CTYPE_APP_JS = "application/javascript"
    __CTYPE_TEXT_CSS_UTF8 = "text/css; charset=utf-8"
    __CTYPE_SVG_XML = "image/svg+xml"

    __RSS_ICON_SVG = {
        'content_type': __CTYPE_SVG_XML,
        'content': """<?xml version="1.0" encoding="UTF-8"?>
<svg xmlns="http://www.w3.org/2000/svg"
     id="RSSicon"
     viewBox="0 0 8 8" width="256" height="256">

  <title>RSS feed icon</title>

  <style type="text/css">
    .button {stroke: none; fill: orange;}
    .symbol {stroke: none; fill: white;}
  </style>

  <rect   class="button" width="8" height="8" rx="1.5" />
  <circle class="symbol" cx="2" cy="6" r="1" />
  <path   class="symbol" d="m 1,4 a 3,3 0 0 1 3,3 h 1 a 4,4 0 0 0 -4,-4 z" />
  <path   class="symbol" d="m 1,2 a 5,5 0 0 1 5,5 h 1 a 6,6 0 0 0 -6,-6 z" />

</svg>"""}

    __SETTINGS_ICON_SVG = {
        'content_type': __CTYPE_SVG_XML,
        'content': """<svg xmlns="http://www.w3.org/2000/svg" width="48" height="48" viewBox="0 0 48 48">
    <path d="M0 0h48v48h-48z" fill="none"/>
    <path d="M38.86 25.95c.08-.64.14-1.29.14-1.95s-.06-1.31-.14-1.95l4.23-3.31c.38-.3.49-.84.24-1.28l-4-6.93c-.25-.43-.77-.61-1.22-.43l-4.98 2.01c-1.03-.79-2.16-1.46-3.38-1.97l-.75-5.3c-.09-.47-.5-.84-1-.84h-8c-.5 0-.91.37-.99.84l-.75 5.3c-1.22.51-2.35 1.17-3.38 1.97l-4.98-2.01c-.45-.17-.97 0-1.22.43l-4 6.93c-.25.43-.14.97.24 1.28l4.22 3.31c-.08.64-.14 1.29-.14 1.95s.06 1.31.14 1.95l-4.22 3.31c-.38.3-.49.84-.24 1.28l4 6.93c.25.43.77.61 1.22.43l4.98-2.01c1.03.79 2.16 1.46 3.38 1.97l.75 5.3c.08.47.49.84.99.84h8c.5 0 .91-.37.99-.84l.75-5.3c1.22-.51 2.35-1.17 3.38-1.97l4.98 2.01c.45.17.97 0 1.22-.43l4-6.93c.25-.43.14-.97-.24-1.28l-4.22-3.31zm-14.86 5.05c-3.87 0-7-3.13-7-7s3.13-7 7-7 7 3.13 7 7-3.13 7-7 7z"/>
</svg>
"""}

    __GOBACK_ICON_SVG = {
        'content_type': __CTYPE_SVG_XML,
        'content': """<svg viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
    <path d="M5.82843 6.99955L8.36396 9.53509L6.94975 10.9493L2 5.99955L6.94975 1.0498L8.36396 2.46402L5.82843 4.99955H13C17.4183 4.99955 21 8.58127 21 12.9996C21 17.4178 17.4183 20.9996 13 20.9996H4V18.9996H13C16.3137 18.9996 19 16.3133 19 12.9996C19 9.68584 16.3137 6.99955 13 6.99955H5.82843Z"/>
</svg>
"""}
