namespace SerialNumber.Service.App.Ioc
{
    using Autofac;

    using Microsoft.Extensions.Logging;

    public class LoggingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register<ILogger>(
                ctx => ctx.Resolve<ILoggerFactory>().CreateLogger("Error Pipeline"))
                .As<ILogger>()
                .SingleInstance();
        }
    }
}