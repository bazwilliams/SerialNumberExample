namespace SerialNumber.Service.Lambda.Console
{
    using System;

    using Amazon.Lambda.Core;

    public class LocalLambdaContext : ILambdaContext
    {
        public LocalLambdaContext(ILambdaLogger logger)
        {
            this.Logger = logger;
        }

        public string AwsRequestId { get; }

        public IClientContext ClientContext { get; }

        public string FunctionName { get; }

        public string FunctionVersion { get; }

        public ICognitoIdentity Identity { get; }

        public string InvokedFunctionArn { get; }

        public ILambdaLogger Logger { get; }

        public string LogGroupName { get; }

        public string LogStreamName { get; }

        public int MemoryLimitInMB { get; }

        public TimeSpan RemainingTime { get; }
    }
}