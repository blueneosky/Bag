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


def read_metadata(media_directory):
    try:
        metadata_directory = ensure_metadata_directory(media_directory)
    except OSError as e:
        return None, None, (jsonify({"error": f"could not access metadata folder {e}"}), 500)

    metadata_path = metadata_directory / ".data.json"
    try:
        if metadata_path.exists():
            with metadata_path.open(encoding="utf-8") as metadata_file:
                metadata = json.load(metadata_file)
            if not isinstance(metadata, dict):
                raise ValueError("metadata must contain a JSON object")
        else:
            metadata = {}
    except (OSError, json.JSONDecodeError, ValueError) as e:
        return None, None, (jsonify({"error": f"could not read metadata file {e}"}), 500)

    return metadata, metadata_path, None


def save_metadata(metadata_path, metadata):
    try:
        with metadata_path.open("w", encoding="utf-8") as metadata_file:
            json.dump(metadata, metadata_file, ensure_ascii=False, indent=2)
            metadata_file.write("\n")
    except OSError as e:
        return jsonify({"error": f"could not save metadata file {e}"}), 500
    return None


def get_file_metadata(metadata, file_name):
    changed = False
    if file_name not in metadata:
        metadata[file_name] = {}
        changed = True

    file_metadata = metadata[file_name]
    if not isinstance(file_metadata, dict):
        return None, False, (jsonify({"error": f"invalid metadata for {file_name}"}), 500)

    if "rating" not in file_metadata:
        file_metadata["rating"] = 0
        changed = True
    rating = file_metadata["rating"]
    if isinstance(rating, bool) or not isinstance(rating, int) or not 0 <= rating <= 5:
        return None, False, (jsonify({"error": f"invalid rating for {file_name}"}), 500)

    if "tags" not in file_metadata:
        file_metadata["tags"] = []
        changed = True
    if not isinstance(file_metadata["tags"], list) or not all(
        isinstance(tag, str) for tag in file_metadata["tags"]
    ):
        return None, False, (jsonify({"error": f"invalid tags for {file_name}"}), 500)

    return file_metadata, changed, None


def load_file_metadata(media_directory, files):
    metadata, metadata_path, error = read_metadata(media_directory)
    if error:
        return None, error

    changed = False
    for file_entry in files:
        if file_entry["type"] != "file":
            continue

        file_metadata, entry_changed, error = get_file_metadata(metadata, file_entry["name"])
        if error:
            return None, error
        changed = changed or entry_changed
        file_entry["rating"] = file_metadata["rating"]
        file_entry["tags"] = file_metadata["tags"]

    if changed or not metadata_path.exists():
        error = save_metadata(metadata_path, metadata)
        if error:
            return None, error

    return files, None


def file_metadata_response(media_id):
    current_path, error = resolve_media_request(media_id)
    if error:
        return None, None, error
    if not current_path.is_file():
        return None, None, (jsonify({"error": "path must be a video file"}), 404)

    metadata, metadata_path, error = read_metadata(current_path.parent)
    if error:
        return None, None, error

    file_metadata, changed, error = get_file_metadata(metadata, current_path.name)
    if error:
        return None, None, error
    if changed or not metadata_path.exists():
        error = save_metadata(metadata_path, metadata)
        if error:
            return None, None, error

    return current_path, file_metadata, None


@backend_blueprint.route('/api/files/<media_id>/metadata', methods=['GET'])
def get_video_metadata(media_id):
    _, file_metadata, error = file_metadata_response(media_id)
    if error:
        return error
    return jsonify({
        "rating": file_metadata["rating"],
        "tags": file_metadata["tags"],
    })


@backend_blueprint.route('/api/files/<media_id>/metadata/rating', methods=['PUT'])
def update_video_rating(media_id):
    if not request.is_json:
        return jsonify({"error": "request body must be JSON"}), 415
    data = request.get_json()
    rating = data.get("rating") if isinstance(data, dict) else None
    if isinstance(rating, bool) or not isinstance(rating, int) or not 0 <= rating <= 5:
        return jsonify({"error": "rating must be an integer from 0 to 5"}), 400

    current_path, _, error = file_metadata_response(media_id)
    if error:
        return error
    metadata, metadata_path, error = read_metadata(current_path.parent)
    if error:
        return error
    file_metadata, _, error = get_file_metadata(metadata, current_path.name)
    if error:
        return error
    file_metadata["rating"] = rating
    error = save_metadata(metadata_path, metadata)
    if error:
        return error
    return jsonify({"rating": rating})


@backend_blueprint.route('/api/files/<media_id>/metadata/tags', methods=['POST', 'DELETE'])
def update_video_tags(media_id):
    if not request.is_json:
        return jsonify({"error": "request body must be JSON"}), 415
    data = request.get_json()
    tag = data.get("tag") if isinstance(data, dict) else None
    if not isinstance(tag, str) or not tag.strip():
        return jsonify({"error": "tag must be a non-empty string"}), 400
    tag = tag.strip()

    current_path, _, error = file_metadata_response(media_id)
    if error:
        return error
    metadata, metadata_path, error = read_metadata(current_path.parent)
    if error:
        return error
    file_metadata, _, error = get_file_metadata(metadata, current_path.name)
    if error:
        return error
    tags = file_metadata["tags"]
    if request.method == "POST":
        if tag not in tags:
            tags.append(tag)
    elif tag not in tags:
        return jsonify({"error": "tag not found"}), 404
    else:
        tags.remove(tag)

    error = save_metadata(metadata_path, metadata)
    if error:
        return error
    return jsonify({"tags": tags})


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
    files, error = load_file_metadata(current_path, files)
    if error:
        return error

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
