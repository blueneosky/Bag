import io
import json
import hashlib
import os
import subprocess
import threading
from pathlib import Path

from flask import Blueprint, jsonify, request, send_file

backend_blueprint = Blueprint('api', __name__)

local_media_path = Path("/srv").resolve()
preview_cache = {}
preview_cache_lock = threading.Lock()


def resolve_media_path(relative_path):
    requested_path = Path(relative_path)

    if requested_path.is_absolute():
        return None, (jsonify({"error": "path must be relative"}), 400)

    current_path = (local_media_path / requested_path).resolve()
    if current_path != local_media_path and local_media_path not in current_path.parents:
        return None, (jsonify({"error": "path must stay inside the media folder"}), 400)

    return current_path, None


def preview_response(image_bytes, cache_key):
    response = send_file(io.BytesIO(image_bytes), mimetype="image/jpeg")
    etag = hashlib.sha256(repr(cache_key).encode()).hexdigest()
    response.headers["Cache-Control"] = "public, max-age=86400"
    response.headers["ETag"] = f'"{etag}"'
    return response

@backend_blueprint.route('/api/files', methods=['GET'])
def get_files():
    current_path, error = resolve_media_path(request.args.get("path", ""))
    if error:
        return error

    if not current_path.is_dir():
        return jsonify({"error": "path must be an existing folder"}), 404

    files = sorted([
        {
            "name": entry.name,
            "type": "folder" if entry.is_dir(follow_symlinks=False) else "file",
        }
        for entry in os.scandir(current_path)
    ], key=lambda item: (item["type"] != "folder", item["name"].casefold()))
    return jsonify({"files": files})


@backend_blueprint.route('/api/files/preview', methods=['GET'])
def preview_file():
    current_path, error = resolve_media_path(request.args.get("path", ""))
    if error:
        return error

    if not current_path.is_file():
        return jsonify({"error": "path is not a video file"}), 400

    try:
        file_signature = (current_path.stat().st_size, current_path.stat().st_mtime_ns)
    except OSError:
        return jsonify({"error": "could not access video file"}), 404

    cache_key = (str(current_path), file_signature)
    with preview_cache_lock:
        cached_image = preview_cache.get(cache_key)
    if cached_image is not None:
        return preview_response(cached_image, cache_key)

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

    with preview_cache_lock:
        preview_cache[cache_key] = extract.stdout

    return preview_response(extract.stdout, cache_key)


@backend_blueprint.route('/api/files/video', methods=['GET'])
def video_file():
    current_path, error = resolve_media_path(request.args.get("path", ""))
    if error:
        return error

    if not current_path.is_file():
        return jsonify({"error": "path must be a video file"}), 400

    return send_file(current_path, conditional=True)

