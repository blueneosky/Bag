FROM python:3.11-slim

ENV PYTHONUNBUFFERED=1 \
    FLASK_DEBUG=0 \
    PORT=8080 \
    PYTHONPATH=/app

RUN export DEBIAN_FRONTEND=noninteractive; \
    export DEBCONF_NONINTERACTIVE_SEEN=true; \
    apt-get update -qqy && apt-get install -qqy --option Dpkg::Options::="--force-confnew" --no-install-recommends \
    ffmpeg && \
    apt-get --quiet autoremove --yes && \
    apt-get --quiet --yes clean && \
    rm -rf /var/lib/apt/lists/*

WORKDIR /app

RUN groupadd local-media && \
    useradd --create-home --home-dir /app --shell /bin/false --gid local-media local-media

COPY requirements.txt ./
RUN pip install --no-cache-dir -r requirements.txt

COPY . .

EXPOSE 8080

USER local-media

CMD ["gunicorn", "--bind", "0.0.0.0:8080", "app.main:app"]
