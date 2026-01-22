# syntax=docker/dockerfile:1

ARG NODE_VERSION=22.18.0
ARG PYTHON_VERSION=3.13

# ======================================================
# === Stage 1: base image with tools
FROM node:${NODE_VERSION}-alpine AS angular_base

RUN --mount=type=cache,target=/root/.npm npm install -g \
    npm@11.5.2 \
    @angular/cli@20.1.6


# ======================================================
# === Stage 2: builder
FROM angular_base AS angular_builder

WORKDIR /app

COPY frontend/package.json frontend/package-lock.json ./

RUN --mount=type=cache,target=/root/.npm npm ci

COPY frontend/ .

RUN npm run build 


# ======================================================
# === Stage 3: pythonBase
FROM python:${PYTHON_VERSION}-slim AS python_base

WORKDIR /app
COPY backend/requirements.txt .

RUN pip install --no-cache-dir -r requirements.txt


# ======================================================
# === Stage 4.prod: runner
FROM python_base AS runner

WORKDIR /app

COPY backend/src .
RUN chmod a+x ./healthcheck.py
COPY --from=angular_builder /app/dist/*/browser ./static

EXPOSE 8080

ENV UVICORN_HOST="0.0.0.0"
ENV UVICORN_PORT="8080"

CMD ["uvicorn", "main:app"]


# ======================================================
# === Stage 4.dev: python_dev
FROM python_base AS python_dev

EXPOSE 8080

ENV UVICORN_HOST="0.0.0.0"
ENV UVICORN_PORT="8080"

CMD ["uvicorn", "main:app"]


# ======================================================
# === Stage 5.dev: angular_dev
FROM angular_base AS angular_dev

ENV NODE_ENV=development

WORKDIR /app

COPY frontend/package.json frontend/package-lock.json ./

RUN --mount=type=cache,target=/root/.npm npm install

COPY frontend/ .

EXPOSE 4200

CMD ["npm", "start", "--", "--host=0.0.0.0"]
