> Ensure instance count set to 0 and run:

aws cloudformation deploy --stack-name=ecs-compute --template-file=ecs-cluster.yaml --capabilities=CAPABILITY_IAM --parameter-overrides keyName=barryw-london clusterName=hello-cluster

aws ec2 describe-instances
aws ecs list-clusters
aws ecs list-container-instances --cluster hello-cluster

> Update template and increase instance count to 1

aws cloudformation deploy --stack-name=ecs-compute --template-file=ecs-cluster.yaml --capabilities=CAPABILITY_IAM

> Note absence of parameter values

aws ec2 describe-instances
aws ecs list-container-instances --cluster hello-cluster