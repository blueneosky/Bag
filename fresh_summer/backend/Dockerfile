FROM python:3.11-slim

ENV PYTHONUNBUFFERED=1 \
    FLASK_DEBUG=0 \
    PORT=5000 \
    PYTHONPATH=/app

WORKDIR /app

COPY requirements.txt ./
RUN pip install --no-cache-dir -r requirements.txt

COPY . .

EXPOSE 5000

CMD ["gunicorn", "--bind", "0.0.0.0:5000", "app.main:app"]
