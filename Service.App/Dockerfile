FROM mono:latest

EXPOSE 8888

ADD ./bin/Release/App.exe /app/
ADD ./bin/Release/*.dll /app/
ADD ./App.config.template /app/

RUN apt-get update && apt-get install -y gettext-base

CMD envsubst < /app/App.config.template > /app/App.exe.config; mono /app/App.exe
