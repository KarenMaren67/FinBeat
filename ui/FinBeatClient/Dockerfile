# stage 1
FROM node:18-alpine as node
ARG environment=production
WORKDIR /app
COPY ui/FinBeatClient/ .
RUN yarn install
RUN yarn run ng build --configuration $environment
# stage 2
FROM nginx:stable-alpine-slim
COPY --from=node /app/dist /usr/share/nginx/html
COPY ui/FinBeatClient/fast-nginx-default.conf /etc/nginx/conf.d/default.conf
