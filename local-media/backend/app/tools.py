import base64
from pathlib import Path
from flask import jsonify, request

__local_media_path = Path("/srv").resolve()

def local_media_path():
    return __local_media_path


def encode_media_reference(relative_path):
    encoded = base64.urlsafe_b64encode(relative_path.encode("utf-8")).decode("ascii")
    return encoded.rstrip("=")


def decode_media_reference(reference):
    if not reference:
        raise ValueError("media reference is required")

    padding = "=" * (-len(reference) % 4)
    decoded = base64.urlsafe_b64decode(f"{reference}{padding}")
    return decoded.decode("utf-8")


def resolve_media_path(relative_path):
    if relative_path is None:
        relative_path = ""

    requested_path = Path(relative_path)

    if requested_path.is_absolute():
        return None, (jsonify({"error": "path must be relative"}), 400)

    current_path = (local_media_path() / requested_path).resolve()
    if current_path != local_media_path() and local_media_path() not in current_path.parents:
        return None, (jsonify({"error": "path must stay inside the media folder"}), 400)

    return current_path, None


def resolve_media_request(media_id=None):
    if media_id:
        try:
            relative_path = decode_media_reference(media_id)
        except (ValueError, UnicodeDecodeError):
            return None, (jsonify({"error": "invalid file reference"}), 400)
        return resolve_media_path(relative_path)

    return resolve_media_path(request.args.get("path", ""))
