import * as cdk from 'aws-cdk-lib';
import { Construct } from 'constructs';
import * as budgets from 'aws-cdk-lib/aws-budgets';
import * as s3 from 'aws-cdk-lib/aws-s3';
import * as cloudfront from 'aws-cdk-lib/aws-cloudfront';
import * as origins from 'aws-cdk-lib/aws-cloudfront-origins';

export interface ExtendedStackProps extends cdk.StackProps {
  alertEmails: string[]
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
  }
}
