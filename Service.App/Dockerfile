FROM microsoft/dotnet:latest

EXPOSE 8080

ADD ./bin/Release/App.exe /app/
ADD ./bin/Release/*.dll /app/
ADD ./App.config.template /app/

RUN apt-get update && apt-get install -y gettext-base

CMD envsubst < /app/App.config.template > /app/App.exe.config; dotnet run /app/App.exe
LABEL org.label-schema.vendor="Barry John Williams" \
      org.label-schema.build-date="2016-09-12T13:52:13Z" \
      org.label-schema.docker.dockerfile="/Dockerfile" \
      org.label-schema.license="MIT" \
      org.label-schema.name="Serial Number Example" \
      org.label-schema.version="coreclr" \
      org.label-schema.url="https://blog.bjw.me.uk/" \
      org.label-schema.vcs-ref="a5394a5" \
      org.label-schema.vcs-type="Git" \
      org.label-schema.vcs-url="https://github.com/bazwilliams/SerialNumberExample" \
      uk.me.bjw.build-number="coreclr" \
      uk.me.bjw.branch="dev" \
      uk.me.bjw.is-production="false"
