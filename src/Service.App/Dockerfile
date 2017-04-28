FROM microsoft/dotnet:1.1-runtime

COPY bin/Release/netcoreapp1.0/publish/ /app/

EXPOSE 8888/tcp

WORKDIR /app

ARG VCS_REF
ARG VERSION
ARG BUILD_DATE

LABEL org.label-schema.vendor="Barry John Williams" \
      org.label-schema.build-date=$BUILD_DATE \
      org.label-schema.docker.dockerfile="Service.App/Dockerfile" \
      org.label-schema.version=$VERSION \
      org.label-schema.vcs-ref=$VCS_REF \
      org.label-schema.vcs-type="Git" \
      org.label-schema.vcs-url="https://github.com/bazwilliams/SerialNumberExample" \
      org.label-schema.name="Serial Number Example" \
	  org.label-schema.license="MIT"

ENTRYPOINT ["dotnet", "Service.App.dll"]
