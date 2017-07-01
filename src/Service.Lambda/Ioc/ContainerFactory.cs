namespace SerialNumber.Service.Lambda.Ioc
{
    using Amazon.Lambda.Core;

    using Autofac;

    using SerialNumber.Service.Ioc;

    internal static class ContainerFactory
    {
        public static IContainer Create(ILambdaLogger lambdaLog)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<ServiceModule>();

            return builder.Build();
        }
    }
}
