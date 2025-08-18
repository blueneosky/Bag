# syntax=docker/dockerfile:1

ARG NODE_VERSION=22.18.0
ARG NGINX_VERSION=alpine3.22

# === Stage 1: base image with tools
FROM node:${NODE_VERSION}-alpine AS base

RUN --mount=type=cache,target=/root/.npm npm install -g \
    npm@11.5.2 \
    @angular/cli@20.1.6

# === Stage 2: builder
FROM base AS builder

WORKDIR /app

COPY package.json package-lock.json ./

RUN --mount=type=cache,target=/root/.npm npm ci

COPY . .

RUN npm run build 

# === Stage 2: runner
FROM nginxinc/nginx-unprivileged:${NGINX_VERSION} AS runner

USER nginx

COPY nginx.conf /etc/nginx/nginx.conf

COPY --chown=nginx:nginx --from=builder /app/dist/*/browser /usr/share/nginx/html

EXPOSE 8080

# Start Nginx directly with custom config
ENTRYPOINT ["nginx", "-c", "/etc/nginx/nginx.conf"]
CMD ["-g", "daemon off;"]