# Test-Library-Shop
Для запуска надо открыть решение в visual studio, сверху вместо https выбрать Container (Dockerfile)
Далее в браузере в адресную строку к открытой странице добавить /swagger/index.html (например https://localhost:32771/swagger/index.html)
Для авторизации надо ввести Bearer [AccessToken] (например Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjcwMjRlZTE5LTM1ZTgtNDkxNi04MGI2LTVhZmVlYzk0OWE1OCIsIm5hbWUiOiJzdHJpbmcxIiwicm9sZSI6IkFkbWluIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiQWRtaW4iLCJleHAiOjE3NDM5NzY4MTgsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjcwMTEiLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo1MDQ0OCJ9.2uFkUTSBeNJBnu9InAhIv24moTMWM52UDNNEJAdgjxM)
Все пользователи создаются user, база данных sqlite, есть admin с name: string1 password: string