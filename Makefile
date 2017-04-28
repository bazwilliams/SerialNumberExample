DOCKER_NAME := bazwilliams/serial-number-example
BUILD_DATE :=`date -u +"%Y-%m-%dT%H:%M:%SZ"`
VCS_REF :=`git rev-parse --short HEAD`

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

all: | build

clean: mostlyclean
	-@rm -rfv packages
	-@rm -rfv tools
	-@find -type d -name project.lock.json -exec rm {} \;

mostlyclean:
	-@find -type d -name bin -exec rm -vrf {} \;
	-@find -type d -name obj -exec rm -vrf {} \;

test:
	@find ./Tests/ -type f -name project.json -print0 | xargs -0 -n 1 dotnet test

build: mostlyclean
	dotnet restore -s https://www.myget.org/F/nancyfx/api/v2/ -s https://www.nuget.org/api/v2/
	dotnet build src/Service.App/ --configuration Release
	dotnet publish src/Service.App/ --configuration Release

all-the-dockers:
	docker build -t $(DOCKER_NAME):$(TRAVIS_BUILD_NUMBER) \
		--build-arg VCS_REF=$(VCS_REF) \
		--build-arg VERSION=$(TRAVIS_BUILD_NUMBER) \
		--build-arg BUILD_DATE=$(BUILD_DATE) \
	src/Service.App

docker-tag:
	$(call tag_docker, $(DOCKER_NAME))

docker-push:
	docker push $(DOCKER_NAME)