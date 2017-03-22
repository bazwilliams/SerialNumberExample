Add this:

  ecsServiceRole:
    Type: "AWS::IAM::Role"
    Properties:
      AssumeRolePolicyDocument:
        Statement:
          - Effect: "Allow"
            Principal:
              Service:
                - "ec2.amazonaws.com"
            Action:
              - "sts:AssumeRole"
      Path: "/"
      
  ecsServiceRolePolicies:
    Type: "AWS::IAM::Policy"
    Properties:
      PolicyName: "ecsInstancePolicy"
      PolicyDocument:
        Statement:
          Effect: "Allow"
          Action:
            - "ec2:AuthorizeSecurityGroupIngress"
            - "ec2:Describe*"
            - "elasticloadbalancing:DeregisterInstancesFromLoadBalancer"
            - "elasticloadbalancing:DeregisterTargets"
            - "elasticloadbalancing:Describe*"
            - "elasticloadbalancing:RegisterInstancesWithLoadBalancer"
            - "elasticloadbalancing:RegisterTargets"
          Resource: "*"
      Roles:
        - !Ref ecsServiceRole

Add to the ecs Service:
        #Role: !Ref ecsServiceRole