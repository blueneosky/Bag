import json
import os
import subprocess
from pathlib import Path

from flask import Blueprint, jsonify, request, send_file, url_for

from app.tools import (
    encode_media_reference,
    ensure_metadata_directory,
    local_media_path,
    resolve_media_path,
    resolve_media_request,
)

backend_blueprint = Blueprint('api', __name__)


def preview_response(image_path):
    response = send_file(image_path, mimetype="image/jpeg", conditional=True)
    response.headers["Cache-Control"] = "public, max-age=86400"
    return response


@backend_blueprint.route('/api/files', methods=['GET'])
def get_files():
    current_path, error = resolve_media_path(request.args.get("path", ""))
    if error:
        return error

    if not current_path.is_dir():
        return jsonify({"error": "path must be an existing folder"}), 404

    def build_file_entry(entry):
        relative_path = Path(os.path.relpath(entry.path, local_media_path())).as_posix()
        return {
            "name": entry.name,
            "type": "folder" if entry.is_dir(follow_symlinks=False) else "file",
            "id": encode_media_reference(relative_path),
        }

    files = sorted(
        [
            build_file_entry(entry)
            for entry in os.scandir(current_path)
            if entry.name != ".local-media"
        ],
        key=lambda item: (item["type"] != "folder", item["name"].casefold()),
    )
    folder_path = current_path.relative_to(local_media_path()).as_posix()
    if folder_path == ".":
        folder_path = ""

    return jsonify({
        "files": files,
        "folder_url": url_for("frontend.index", path=folder_path),
    })


@backend_blueprint.route('/api/files/<media_id>', methods=['GET'])
def video_file(media_id=None):
    current_path, error = resolve_media_request(media_id)
    if error:
        return error

    if not current_path.is_file():
        return jsonify({"error": "path must be a video file"}), 400

    return send_file(current_path, conditional=True)



@backend_blueprint.route('/api/files/preview/<media_id>', methods=['GET'])
def preview_file(media_id=None):
    current_path, error = resolve_media_request(media_id)
    if error:
        return error

    if not current_path.is_file():
        return jsonify({"error": "path is not a video file"}), 400

    try:
        metadata_directory = ensure_metadata_directory(current_path.parent)
    except OSError as e:
        return jsonify({"error": f"could not access metadata folder {e}"}), 500

    preview_path = metadata_directory / f"{current_path.stem}.jpeg"
    if preview_path.is_file():
        return preview_response(preview_path)

    probe = subprocess.run(
        [
            "ffprobe",
            "-v", "error",
            "-select_streams", "v:0",
            "-show_entries", "stream=codec_type:format=duration",
            "-of", "json",
            str(current_path),
        ],
        capture_output=True,
        text=True,
        check=False,
    )
    if probe.returncode != 0:
        return jsonify({"error": "path is not a video file"}), 400

    try:
        probe_data = json.loads(probe.stdout)
        if not probe_data.get("streams") or probe_data["streams"][0].get("codec_type") != "video":
            return jsonify({"error": "path is not a video file"}), 400
        duration = float(probe_data["format"]["duration"])
    except (KeyError, TypeError, ValueError, json.JSONDecodeError):
        return jsonify({"error": "could not determine video duration"}), 422

    timestamp = duration / 3 if duration < 20 * 60 else 10 * 60
    extract = subprocess.run(
        [
            "ffmpeg",
            "-v", "error",
            "-ss", str(timestamp),
            "-i", str(current_path),
            "-frames:v", "1",
            "-vf", "scale='min(128,iw)':-1",
            "-f", "image2pipe",
            "-vcodec", "mjpeg",
            "-q:v", "5",
            "pipe:1",
        ],
        capture_output=True,
        check=False,
    )
    if extract.returncode != 0 or not extract.stdout:
        return jsonify({"error": "could not extract video preview"}), 422

    try:
        preview_path.write_bytes(extract.stdout)
    except OSError:
        return jsonify({"error": "could not save video preview"}), 500

    return preview_response(preview_path)
