FROM python:3.13-slim

# set a directory for the app
WORKDIR /app

# copy all the files to the container
COPY src .
RUN chmod a+x ./healthcheck.py

# install dependencies
RUN pip install --no-cache-dir -r requirements.txt

# define the port number the container should expose
# EXPOSE 6110
EXPOSE 8000

ENV UVICORN_HOST="0.0.0.0"
ENV UVICORN_PORT="8000"

CMD ["uvicorn", "main:app"]
