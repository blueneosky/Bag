from flask import Blueprint, render_template

from app.tools import local_media_path, resolve_media_request

frontend_blueprint = Blueprint(
    "frontend",
    __name__,
    template_folder="templates",
    url_prefix="/front",
)


@frontend_blueprint.route("", methods=["GET"], strict_slashes=False)
def index():
    return render_template("front.html")


@frontend_blueprint.route("/player/<media_id>", methods=["GET"])
def player(media_id=None):
    current_path, error = resolve_media_request(media_id)
    if error:
        return "Video not found", 404

    folder_path = current_path.parent.relative_to(local_media_path()).as_posix()
    if folder_path == ".":
        folder_path = ""

    return render_template(
        "player.html",
        path=current_path.relative_to(local_media_path()),
        folder_path=folder_path,
        id=media_id,
    )
