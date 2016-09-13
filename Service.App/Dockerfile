FROM microsoft/dotnet:latest

COPY bin/Release/netcoreapp1.0/publish/ /app/

EXPOSE 8888/tcp

ENTRYPOINT dotnet /app/Service.App.dll
