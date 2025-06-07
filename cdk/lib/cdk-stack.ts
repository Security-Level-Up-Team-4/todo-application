import * as cdk from 'aws-cdk-lib';
import { Construct } from 'constructs';
import * as budgets from 'aws-cdk-lib/aws-budgets';
import * as s3 from 'aws-cdk-lib/aws-s3';
import * as ec2 from 'aws-cdk-lib/aws-ec2';
import * as cloudfront from 'aws-cdk-lib/aws-cloudfront';
import * as origins from 'aws-cdk-lib/aws-cloudfront-origins';
import * as rds from 'aws-cdk-lib/aws-rds';
import * as iam from 'aws-cdk-lib/aws-iam';
import * as elasticbeanstalk from 'aws-cdk-lib/aws-elasticbeanstalk';

export interface ExtendedStackProps extends cdk.StackProps {
  alertEmails: string[];
  instanceType: string;
  minInstances: number;
  maxInstances: number;
}

export class CdkStack extends cdk.Stack {
  constructor(scope: Construct, id: string, props: ExtendedStackProps) {
    super(scope, id, props);

    const maxBudgetAmount = 50.00;
    const forecastedThresholds = [50, 75]
    const actualThresholds = [10, 20, 30, 40, 60, 80, 90, 100];

    new budgets.CfnBudget(this, 'Team4Budget', {
      budget: {
        budgetName: 'Team-4-LevelUp-Budget',
        budgetType: 'COST',
        timeUnit: 'MONTHLY',
        budgetLimit: {
          amount: maxBudgetAmount,
          unit: 'USD',
        },
      },
      notificationsWithSubscribers: [
        ...forecastedThresholds.map(percent => ({
          notification: {
            comparisonOperator: 'GREATER_THAN',
            notificationType: 'FORECASTED',
            threshold: percent,
            thresholdType: 'PERCENTAGE',
            notificationState: 'ALARM'
          },

          subscribers: props.alertEmails.map(email => ({
            subscriptionType: 'EMAIL',
            address: email
          }))
        })),
        ...actualThresholds.map(percent => ({
          notification: {
            comparisonOperator: 'GREATER_THAN',
            notificationType: 'ACTUAL',
            threshold: percent,
            thresholdType: 'PERCENTAGE',
            notificationState: 'ALARM'
          },

          subscribers: props.alertEmails.map(email => ({
            subscriptionType: 'EMAIL',
            address: email
          }))
        }))
      ]
    });

    const bucketName = `security-levelup-team4-bucket`;
    const team4Bucket = new s3.Bucket(this, bucketName, {
      bucketName: bucketName,
      websiteIndexDocument: 'index.html',
      websiteErrorDocument: 'index.html',
      accessControl: s3.BucketAccessControl.PRIVATE,
      blockPublicAccess: s3.BlockPublicAccess.BLOCK_ALL,
      removalPolicy: cdk.RemovalPolicy.DESTROY,
      autoDeleteObjects: true
    });

    const distribution = new cloudfront.Distribution(this, 'security-levelup-team4-distribution', {
      defaultBehavior: {
        origin: origins.S3BucketOrigin.withOriginAccessControl(team4Bucket, {
          originAccessLevels: [cloudfront.AccessLevel.READ],
        }),
        viewerProtocolPolicy: cloudfront.ViewerProtocolPolicy.REDIRECT_TO_HTTPS,
        cachePolicy: cloudfront.CachePolicy.CACHING_DISABLED,
      },
      defaultRootObject: 'index.html',
      errorResponses: [
        {
          httpStatus: 403, 
          responseHttpStatus: 200,
          responsePagePath: '/index.html',
        },
        {
          httpStatus: 404,
          responseHttpStatus: 200,
          responsePagePath: '/index.html',
        },
      ],
    });

    const vpc = new ec2.Vpc(this, 'VPC', {
      maxAzs: 2,
      natGateways: 0,
      subnetConfiguration: [
        {
          cidrMask: 24,
          name: 'public-subnet',
          subnetType: ec2.SubnetType.PUBLIC,
        },
      ],
    });

    const dbSecurityGroup = new ec2.SecurityGroup(this, 'DatabaseSecurityGroup', {
      vpc,
      description: 'Security group for RDS instance',
      allowAllOutbound: true,
    });

    const team4DbName = `team4TodoDB`;
    const database = new rds.DatabaseInstance(this, team4DbName, {
      engine: rds.DatabaseInstanceEngine.postgres({
        version: rds.PostgresEngineVersion.VER_16,
      }),
      instanceType: ec2.InstanceType.of(ec2.InstanceClass.T3, ec2.InstanceSize.MICRO),
      vpc,
      vpcSubnets: {
        subnetType: ec2.SubnetType.PUBLIC,
      },
      publiclyAccessible: true,
      securityGroups: [dbSecurityGroup],
      databaseName: team4DbName,
      allocatedStorage: 20,
      maxAllocatedStorage: 70,
      allowMajorVersionUpgrade: false,
      autoMinorVersionUpgrade: true,
      deleteAutomatedBackups: true,
      removalPolicy: cdk.RemovalPolicy.DESTROY,
      deletionProtection: false,
    });
    
    const todoRoleName = `team4-iam-role`;
    const ebInstanceRole = new iam.Role(this, todoRoleName, {
      roleName: todoRoleName,
      assumedBy: new iam.ServicePrincipal('ec2.amazonaws.com'),
    });

    ebInstanceRole.addManagedPolicy(
      iam.ManagedPolicy.fromAwsManagedPolicyName('AWSElasticBeanstalkWebTier')
    );

    ebInstanceRole.addToPolicy(new iam.PolicyStatement({
      effect: iam.Effect.ALLOW,
      actions: [
        'rds-db:connect',
        'rds:DescribeDBInstances',
      ],
      resources: [database.instanceArn],
    }));

    const instanceProfile = new iam.CfnInstanceProfile(this, 'InstanceProfile', {
      roles: [ebInstanceRole.roleName],
    });

    const ebSecurityGroup = new ec2.SecurityGroup(this, 'EBSecurityGroup', {
      vpc,
      description: 'Security group for Elastic Beanstalk instances',
      allowAllOutbound: true,
    });

    dbSecurityGroup.addIngressRule(
      ebSecurityGroup,
      ec2.Port.tcp(5432),
      'Allow Elastic Beanstalk to connect to PostgreSQL'
    );

    // Allow public access to the database for testing purposes
    dbSecurityGroup.addIngressRule(
      ec2.Peer.anyIpv4(),
      ec2.Port.tcp(5432),
      'Allow public access to PostgreSQL'
    );

    const app = new elasticbeanstalk.CfnApplication(this, 'Application', {
      applicationName: 'team4-todo-api',
    });

    const environment = new elasticbeanstalk.CfnEnvironment(this, 'Environment', {
      environmentName: 'team4-todo-api-env',
      applicationName: app.applicationName || 'team4-todo-api',
      solutionStackName: '64bit Amazon Linux 2023 v3.4.2 running .NET 9',
      optionSettings: [
        {
          namespace: 'aws:autoscaling:launchconfiguration',
          optionName: 'IamInstanceProfile',
          value: instanceProfile.attrArn,
        },
        {
          namespace: 'aws:autoscaling:launchconfiguration',
          optionName: 'InstanceType',
          value: props.instanceType,
        },
        {
          namespace: 'aws:autoscaling:launchconfiguration',
          optionName: 'SecurityGroups',
          value: ebSecurityGroup.securityGroupId,
        },
        {
          namespace: 'aws:autoscaling:asg',
          optionName: 'MinSize',
          value: props.minInstances.toString(),
        },
        {
          namespace: 'aws:autoscaling:asg',
          optionName: 'MaxSize',
          value: props.maxInstances.toString(),
        },
        {
          namespace: 'aws:elasticbeanstalk:environment',
          optionName: 'EnvironmentType',
          value: 'SingleInstance',
        },
        {
          namespace: 'aws:elasticbeanstalk:environment',
          optionName: 'ServiceRole',
          value: 'aws-elasticbeanstalk-service-role',
        },
        {
          namespace: 'aws:elasticbeanstalk:environment:process:default',
          optionName: 'HealthCheckPath',
          value: '/health',
        },
        {
          namespace: 'aws:ec2:vpc',
          optionName: 'VPCId',
          value: vpc.vpcId,
        },
        {
          namespace: 'aws:ec2:vpc',
          optionName: 'Subnets',
          value: vpc.publicSubnets.map(subnet => subnet.subnetId).join(','),
        },
        {
          namespace: 'aws:ec2:vpc',
          optionName: 'ELBSubnets',
          value: vpc.publicSubnets.map(subnet => subnet.subnetId).join(','),
        },
      ],
    });

  }
}
