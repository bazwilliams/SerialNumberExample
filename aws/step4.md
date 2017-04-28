aws cloudformation deploy --stack-name=loadbalancer --template-file=loadbalancer.yaml

aws cloudformation deploy --stack-name=serial-number-example-application --template-file=ecs-application-alb.yaml --capabilities=CAPABILITY_IAM
