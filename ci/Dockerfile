FROM debian:stable

ENV DOTNET_SKIP_FIRST_TIME_EXPERIENCE=1

RUN apt-get update; apt-get install -y curl libunwind8 gettext apt-transport-https gpg make zip unzip python3-pip
RUN curl https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor > /etc/apt/trusted.gpg.d/microsoft.gpg
RUN echo "deb [arch=amd64] https://packages.microsoft.com/repos/microsoft-debian-stretch-prod stretch main" > /etc/apt/sources.list.d/dotnetdev.list
RUN apt-get update; apt-get install -y dotnet-sdk-2.0.0
RUN pip3 install awscli --upgrade