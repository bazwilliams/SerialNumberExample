namespace SerialNumber.Service.Lambda.Ioc
{
    using Amazon.Lambda.Core;

    using Autofac;

    using SerialNumber.Service.Ioc;

    public static class LambdaContainerFactory
    {
        public static IContainer Create(ILambdaLogger lambdaLogger)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<ServiceModule>();
            return builder.Build();
        }
    }
}
