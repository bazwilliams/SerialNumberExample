DOCKER_NAME := bazwilliams/serial-number-service
SOLUTION := SerialNumberExample.sln
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

define label_dockerfile
	@echo "LABEL org.label-schema.vendor=\"Barry John Williams\" \\" >> $(1)
	@echo "      org.label-schema.build-date=\"$(BUILD_DATE)\" \\" >> $(1)
	@echo "      org.label-schema.docker.dockerfile=\"/Dockerfile\" \\" >> $(1)
	@echo "      org.label-schema.license=\"MIT\" \\" >> $(1)
	@echo "      org.label-schema.name=\"Serial Number Example\" \\" >> $(1)
	@echo "      org.label-schema.version=\"$(TRAVIS_BUILD_NUMBER)\" \\" >> $(1)
	@echo "      org.label-schema.url=\"https://blog.bjw.me.uk/\" \\" >> $(1)
	@echo "      org.label-schema.vcs-ref=\"$(VCS_REF)\" \\" >> $(1)
	@echo "      org.label-schema.vcs-type=\"Git\" \\" >> $(1)
	@echo "      org.label-schema.vcs-url=\"https://github.com/bazwilliams/SerialNumberExample\" \\" >> $(1)
	@echo "      uk.me.bjw.build-number=\"$(TRAVIS_BUILD_NUMBER)\" \\" >> $(1)
	@echo "      uk.me.bjw.branch=\"$(TRAVIS_BRANCH)\" \\" >> $(1)
	@if [ "$(TRAVIS_BRANCH)" = "master" -a "$(TRAVIS_PULL_REQUEST)" = "false" ]; then \
		echo "      uk.me.bjw.is-production=\"true\"" >> $(1); \
	else \
		echo "      uk.me.bjw.is-production=\"false\"" >> $(1); \
	fi
endef

all: | build

clean: mostlyclean
	-@rm -fv $(NUGET)
	-@rm -fv $(NUNIT_RUNNER)
	-@rm -rfv packages
	-@rm -rfv tools

mostlyclean:
	-@find -type f -name App.config -exec rm -vf {} \;
	-@find -type d -name bin -exec rm -vrf {} \;
	-@find -type d -name obj -exec rm -vrf {} \;

build: mostlyclean
	dotnet restore
	dotnet build Service.App/
	
$(DOCKER_NAME):
	$(call label_dockerfile, Service.App/Dockerfile)
	docker build -q -t $(DOCKER_NAME):$(TRAVIS_BUILD_NUMBER) Service.App

all-the-dockers: $(DOCKER_NAME)

docker-tag:
	$(call tag_docker, $(DOCKER_NAME))

docker-push:
	docker push $(DOCKER_NAME)
