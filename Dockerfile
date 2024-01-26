#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
#
#FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
#WORKDIR /app

#
#FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
#WORKDIR /src
#COPY ["UserStore.API.csproj", "UserStore.API/"]
#COPY ["UserStore.Application.csproj", "UserStore.Application/"]
##COPY ["UserStore.Core/UserStore.Core.csproj", "UserStore.Core/"]
##COPY ["UserStore.DataAccess/UserStore.DataAccess.csproj", "UserStore.DataAccess/"]
#RUN dotnet restore "UserStore.API/UserStore.API.csproj"
#COPY . .
#WORKDIR "/src/UserStore.API"
#RUN dotnet build "UserStore.API.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "UserStore.API.csproj" -c Release -o /app/publish /p:UseAppHost=false
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "UserStore.API.dll"]


# Сначала установим SDK для сборки проектов

#FROM docker.io/library/build:latest AS base
#FROM slavakunz/todo:tagname AS base
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

EXPOSE 80
EXPOSE 443

WORKDIR /app

# Копируем файлы проектов в рабочую директорию
COPY . .

# Копируем файл UserStore.API.sln внутрь контейнера
# COPY UserStore.API.sln /app

RUN echo "Contents of /app directory:"
RUN ls /app

#RUN test -e UserStore.API.sln || (echo "File UserStore.API.sln not found!" && exit 1)
# Собираем все проекты
RUN dotnet build UserStore.API/UserStore.API.sln -c Release

# Запускаем сборку и публикацию проектов
RUN dotnet publish UserStore.API/UserStore.API.csproj -c Release -o /app/publish/UserStore.API
RUN dotnet publish UserStore.Application/UserStore.Application.csproj -c Release -o /app/publish/UserStore.Application
RUN dotnet publish UserStore.Core/UserStore.Core.csproj -c Release -o /app/publish/UserStore.Core
RUN dotnet publish UserStore.DataAccess/UserStore.DataAccess.csproj -c Release -o /app/publish/UserStore.DataAccess

# Второй этап собирает конечный образ приложения
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app




# Копируем скомпилированные проекты из предыдущего этапа
COPY --from=build /app/publish/UserStore.API .
COPY --from=build /app/publish/UserStore.Application .
COPY --from=build /app/publish/UserStore.Core .
COPY --from=build /app/publish/UserStore.DataAccess .

# Запускаем проекты
CMD ["dotnet", "UserStore.API.dll"]