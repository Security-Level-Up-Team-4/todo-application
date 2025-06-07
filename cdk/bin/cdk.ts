#!/usr/bin/env node
import * as cdk from 'aws-cdk-lib';
import { CdkStack } from '../lib/cdk-stack';

const app = new cdk.App();

const recipients = [
  'Alfred.Malope@bbd.co.za',
  'Vuyo.Sibiya@bbd.co.za',
  'Ryan.Christie@bbd.co.za',
  'Tshepo.Ncube@bbd.co.za',
  'Rorisang.Shadung@bbd.co.za',
  'rudolphe@bbd.co.za'
]

new CdkStack(app, 'TodoCdkStack-prod', {
  stackName: 'team4-aws-infrastructure',
  description: 'Team 4 - AWS CDK Stack',
  env: {
    account: process.env.CDK_DEFAULT_ACCOUNT,
    region: "us-east-1",
  },
  alertEmails: recipients,
  instanceType: 't3.micro',
  minInstances: 1,
  maxInstances: 1,
});