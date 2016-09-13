FROM microsoft/dotnet:latest

COPY bin/Debug/netcoreapp1.0/publish/ /app/

EXPOSE 8888/tcp

ENTRYPOINT dotnet /app/Service.App.dll
