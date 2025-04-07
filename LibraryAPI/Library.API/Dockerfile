# Используем официальный образ .NET SDK в качестве базового для сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Копируем файл проекта и восстанавливаем зависимости
COPY Library.API.csproj ./
RUN dotnet restore

# Копируем весь остальной исходный код
COPY . ./

# Публикуем приложение
RUN dotnet publish -c Release -o out

# Используем официальный образ ASP.NET Core Runtime для запуска
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "Library.API.dll"]