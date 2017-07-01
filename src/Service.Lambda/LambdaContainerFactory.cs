namespace SerialNumber.Service.Lambda.Ioc
{
    using Amazon.Lambda.Core;

    using Autofac;

    using SerialNumber.Service.Ioc;

    public class LambdaContainerFactory : IContainerFactory
    {
        private readonly ILambdaLogger lambdaLog;

        public LambdaContainerFactory(ILambdaLogger lambdaLog)
        {
            this.lambdaLog = lambdaLog;
        }

        public IContainer Create()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<ServiceModule>();
            return builder.Build();
        }
    }
}
