# stage 1 - Сборка Node.js приложения
FROM node:18-alpine as node
ARG environment=production
WORKDIR /app

# Копируем только файлы package, чтобы Docker мог кэшировать npm install
COPY ui/FinBeatClient/package.json ./
COPY ui/FinBeatClient/package-lock.json ./
RUN npm install

# Копируем остальной код Angular приложения
COPY ui/FinBeatClient/ .

# Собираем Angular приложение
# Указываем outputPath, который подтвержден локально: ./dist/fin-beat-client
# Добавляем --base-href=/ для корректной работы маршрутизации SPA
RUN npm run build -- FinBeatClient --configuration=$environment --output-path=./dist/fin-beat-client --base-href=/

# stage 2 - Сервер Nginx
FROM nginx:stable-alpine-slim

# Копируем скомпилированное Angular-приложение из этапа сборки
# Источник: /app/dist (путь внутри контейнера сборки)
# Назначение: /usr/share/nginx/html (корневая папка Nginx)
COPY --from=node /app/dist /usr/share/nginx/html

# Копируем файл конфигурации Nginx
COPY ui/FinBeatClient/fast-nginx-default.conf /etc/nginx/conf.d/default.conf

# Опционально: очистка кэша Nginx и настройка прав для логов (хорошая практика)
RUN rm -rf /var/cache/apk/* && \
    mkdir -p /var/log/nginx && \
    chmod 755 /var/log/nginx

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]