Запуск в докере
1. Запускать:
    ```shell
    docker compose up -d --build
    ```
   Порты 80 и 443 должны быть не заняты.
2. Перейти по адресу https://localhost

Запуск локально
1. Запустить Postgres:
   ```shell
   docker compose -f .\docker-compose.dev.yml -f .\docker-compose.yml up -d postgres
   ```
2. Запустить проекты TaskManager.Api: http и TaskManager.UI: http
   > Фронтенд: http://localhost:4200
   > 
   > Бэкенд: http://localhost:5000


