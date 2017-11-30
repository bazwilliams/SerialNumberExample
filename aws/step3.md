

aws cloudformation deploy --stack-name=serial-number-example-application --template-file=./ecs-application.yaml  --capabilities=CAPABILITY_IAM --parameter-overrides targetCluster=hello-cluster dockerTag=dynamodb

> Now find ip address of the host machine and post another serial number request to that IP address. You should see the next serial number. The DynamoDb table ARN has been imported into the policy used for the application, we've used a restricted role for the task and the table name has been imported into the application has an environment variable.

> Downsides: We've restricted ourselves because we've explicitly set the host port 8080 rather than use a randomly selected port from the ephemeral port range. Secondly our security group has left our machine wide open to the world. Lets fix those issues. 