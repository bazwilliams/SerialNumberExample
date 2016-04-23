FROM mono:latest

ADD . /usr/src/app/
WORKDIR /usr/src/app

ADD https://dist.nuget.org/win-x86-commandline/latest/nuget.exe /usr/src/app/packages


RUN apt-get update && apt-get install -y vim make

CMD /bin/bash
