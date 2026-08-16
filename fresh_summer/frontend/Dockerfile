# Stage 1: Build
FROM node:22 as build-stage
WORKDIR /app
COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build -- --configuration=production

# Stage 2: Run
FROM nginx:alpine
COPY --from=build-stage /app/dist/fresh_summer/browser /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
