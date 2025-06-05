import * as cdk from 'aws-cdk-lib';
import { Construct } from 'constructs';
import * as budgets from 'aws-cdk-lib/aws-budgets';

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
      notificationsWithSubscribers:[
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
  }
}
