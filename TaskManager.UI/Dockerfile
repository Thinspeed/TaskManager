FROM nginx:1.27.4-alpine AS base
WORKDIR /var/www/web
USER $APP_UID
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TaskManager.UI/TaskManager.UI.csproj", "TaskManager.UI/"]
COPY ["TaskManager.UI.Infrastructure/TaskManager.UI.Infrastructure.csproj", "TaskManager.UI.Infrastructure/"]
RUN dotnet restore "TaskManager.UI/TaskManager.UI.csproj"
COPY . .
WORKDIR "/src/TaskManager.UI"
RUN dotnet build "TaskManager.UI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "TaskManager.UI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
COPY --from=publish /app/publish/wwwroot .
COPY ["TaskManager.UI/nginx.conf", "/etc/nginx/nginx.conf"]