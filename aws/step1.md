aws cloudformation deploy --stack-name=serial-number-example-persistence --template-file=persistence.yaml

aws cloudformation describe-stacks --stack-name=serial-number-example-persistence

docker run --rm -p 8080:8080 -e AWS_ACCESS_KEY_ID=$AWS_ACCESS_KEY_ID -e AWS_SECRET_ACCESS_KEY=$AWS_SECRET_ACCESS_KEY -e AWS_REGION=eu-west-2 -e sequencesTableName= bazwilliams/serial-number-example:dynamodb

> We've demonstrated creating some dynamodb resource, exporting the table name created and using that within an external application. We are using credentials which have already been given write permission to this table, by virtue of those credentials being used to create the resource in the first place.