# FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine AS base
# WORKDIR /app
# EXPOSE 80
# ENV ASPNETCORE_URLS=http://+:80

# FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build
# WORKDIR /src
# COPY src/api.csproj .
# RUN dotnet restore
# COPY src/ .
# # RUN dotnet build api.csproj -c Release -o /app/build

# FROM build AS publish
# # RUN dotnet publish api.csproj -c Release -o /app/publish

# FROM base AS final
# ENV DOTNET_USE_POLLING_FILE_WATCHER 1
# WORKDIR /app
# # COPY --from=publish /app .
# # ENTRYPOINT ["dotnet", "app.dll"]
# WORKDIR /src
# ENTRYPOINT ["dotnet", "watch", "run"]

FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS base
ENV DOTNET_USE_POLLING_FILE_WATCHER 1
WORKDIR /src
COPY src/ .
RUN dotnet restore

ENTRYPOINT ["dotnet", "watch", "run", "--urls=http://0.0.0.0:5000"]