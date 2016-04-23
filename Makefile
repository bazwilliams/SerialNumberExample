DOCKER_NAME := bazwilliams/serial-number-service
DOCKER_PROJECT := Service.App
SOLUTION := SerialNumberExample.sln

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

nuget:
	mkdir -p ./packages
	wget -O ./packages/nuget.exe https://dist.nuget.org/win-x86-commandline/latest/nuget.exe

nuget-restore:
	mono ./packages/nuget.exe restore $(SOLUTION) 

testrunner:
	mono ./packages/nuget.exe install NUnit.Runners -Version 3.0.1 -o packages

appconfig:
	cp $(DOCKER_PROJECT)/App.config.template $(DOCKER_PROJECT)/App.config
	
compile: clean nuget-restore appconfig
	xbuild /verbosity:minimal /p:TargetFrameworkVersion="v4.5" /p:Configuration=Release $(SOLUTION)
	
test: testrunner
	mono ./packages/NUnit.Console.3.0.1/tools/nunit3-console.exe -workers 1 `(find Tests -name *Tests.dll | grep -v obj/Release)`

$(DOCKER_NAME): compile
	docker build -t $(DOCKER_NAME):$(TRAVIS_BUILD_NUMBER) $(DOCKER_PROJECT)

all-the-dockers: $(DOCKER_NAME)

docker-tag:
	$(call tag_docker, $(DOCKER_NAME))

docker-push:
	docker push $(DOCKER_NAME)
