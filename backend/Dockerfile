FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /api
COPY src/ .
RUN dotnet restore .
RUN dotnet tool restore
RUN dotnet ef dbcontext script -p BabyCareX.Repository -s BabyCareX.WebApi -o dbcontext-migrate.sql
RUN dotnet build .

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS publish
WORKDIR /publish
WORKDIR /build
COPY --from=build /api/ .
RUN dotnet publish . -c Release -o /publish

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS final

LABEL version="1.0" \
description="BabyCareX Web API with data seeding" \
maintainer="Lucio Pereira Brito <luciopereirabritoo@gmail.com>" \
org.opencontainers.image.source="https://github.com/luciopbrito/BabyCareX" \
org.opencontainers.image.documentation="https://github.com/luciopbrito/BabyCareX/blob/main/README.md" \
org.opencontainers.image.licenses="MIT" \
org.opencontainers.image.title="BabyCareX Web API" \
org.opencontainers.image.url="https://github.com/luciopbrito/BabyCareX" \
org.opencontainers.image.vendor="Lucio Pereira Brito" \
org.opencontainers.image.authors="Lucio Pereira Brito <luciopereirabritoo@gmail.com>" \
org.opencontainers.image.version="1.0.0" \
org.opencontainers.image.created="2024-06-01T00:00:00Z"

WORKDIR /app
COPY --from=publish /publish/ .
COPY --from=build /api/dbcontext-migrate.sql .
ENTRYPOINT ["dotnet", "BabyCareX.WebApi.dll"]
