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
	dotnet restore
	dotnet publish src/Service.Lambda/ --configuration Release

package:
	cd ./src/Service.Lambda/bin/Release/netcoreapp1.0/publish/ && zip serial-number-handler.zip -r . *

upload:
	aws s3 cp src/Service.Lambda/bin/Release/netcoreapp1.0/publish/serial-number-handler.zip s3://linn.lambdas.london/serial-number-handler-${LAMBDA_TAG}.zip