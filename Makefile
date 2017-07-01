LAMBDA_NAME := serial-number-example.zip
BUILD_DATE :=`date -u +"%Y-%m-%dT%H:%M:%SZ"`
VCS_REF :=`git rev-parse --short HEAD`

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
	dotnet build src/Service.Lambda/ --configuration Release
	dotnet publish src/Service.Lambda/ --configuration Release
