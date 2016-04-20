SERIAL_NUMBER_DOCKER_NAME := bazwilliams/serial-number-service
DOCKER_BRANCH_TAG := $(shell echo ${TRAVIS_BRANCH} | sed s/\#/_/g)

define tag_docker
	@if [ "$(TRAVIS_BRANCH)" != "master" ]; then \
		docker tag $(1):$(TRAVIS_BUILD_NUMBER) $(1):$(DOCKER_BRANCH_TAG); \
	fi
	@if [ "$(TRAVIS_BRANCH)" = "master" -a "$(TRAVIS_PULL_REQUEST)" = "false" ]; then \
		docker tag $(1):$(TRAVIS_BUILD_NUMBER) $(1):latest; \
	fi
	@if [ "$(TRAVIS_PULL_REQUEST)" != "false" ]; then \
		docker tag $(1):$(TRAVIS_BUILD_NUMBER) $(1):PR_$(TRAVIS_PULL_REQUEST); \
	fi
endef

clean:
	-find -type d -name bin -exec rm -rf {} \;
	-find -type d -name obj -exec rm -rf {} \;

appconfig:
	cp Service.App/App.config.template Service.App/App.config
	
compile: clean appconfig
	xbuild /p:TargetFrameworkVersion="v4.5" /p:Configuration=Release SerialNumberExample.sln
	
test: 
	mono ./packages/NUnit.Console.3.0.1/tools/nunit3-console.exe -workers 1 `(find Tests -name *Tests.dll | grep -v obj/Release)`

$(SERIAL_NUMBER_DOCKER_NAME): compile
	docker build -t $(SERIAL_NUMBER_DOCKER_NAME):$(TRAVIS_BUILD_NUMBER) Service.App

all-the-dockers: $(SERIAL_NUMBER_DOCKER_NAME)

docker-tag:
	$(call tag_docker, $(SERIAL_NUMBER_DOCKER_NAME))

docker-push:
	docker push $(SERIAL_NUMBER_DOCKER_NAME)
