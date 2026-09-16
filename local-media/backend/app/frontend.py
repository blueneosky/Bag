from flask import Blueprint, render_template, request, url_for

frontend_blueprint = Blueprint(
    "frontend",
    __name__,
    template_folder="templates",
    url_prefix="/front",
)


@frontend_blueprint.route("", methods=["GET"], strict_slashes=False)
def index():
    return render_template("front.html")


@frontend_blueprint.route("/player", methods=["GET"])
def player():
    path = request.args.get("path", "")
    if not path:
        return "A video path is required", 400

    return render_template(
        "player.html",
        path=path,
        video_url=url_for("api.video_file", path=path),
    )
