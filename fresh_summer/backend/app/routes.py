from flask import Blueprint, jsonify, request

api_blueprint = Blueprint('api', __name__)

@api_blueprint.route('/api/meteo', methods=['GET'])
def get_meteo():
    return jsonify({"temperature": 25, "humidite": 60, "pression": 1013})

@api_blueprint.route('/api/alerte', methods=['POST'])
def send_alerte():
    data = request.get_json()
    return jsonify({"status": "alerte envoyée", "data": data})
