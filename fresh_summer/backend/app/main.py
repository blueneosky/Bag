import os

from flask import Flask

from app.routes import api_blueprint


def create_app():
    app = Flask(__name__)
    app.config["DEBUG"] = os.getenv("FLASK_DEBUG", "0") == "1"
    app.register_blueprint(api_blueprint)
    return app


app = create_app()

if __name__ == '__main__':
    app.run(
        host="0.0.0.0",
        port=int(os.getenv("PORT", "5000")),
        debug=app.config["DEBUG"],
    )
