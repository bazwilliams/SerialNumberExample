namespace SerialNumber.Service.App
{
    using System;

    using Autofac;

    using Microsoft.Extensions.Logging;

    using Nancy;
    using Nancy.Bootstrapper;
    using Nancy.Bootstrappers.Autofac;

    using SerialNumber.Service.App.Ioc;

    public class Bootstrapper : AutofacNancyBootstrapper
    {
        private readonly ILoggerFactory loggerFactory;

        public Bootstrapper(ILoggerFactory loggerFactory)
        {
            this.loggerFactory = loggerFactory;
        }

        protected override void RequestStartup(ILifetimeScope container, IPipelines pipelines, NancyContext context)
        {
            base.RequestStartup(container, pipelines, context);

            pipelines.AfterRequest += ctx => ctx.Response.Headers.Add("Access-Control-Allow-Origin", "*");

            pipelines.OnError += (ctx, ex) =>
            {
                Log(ex, container.Resolve<ILogger>());

                ctx.Items.Add("OnErrorException", ex);

                return null;
            };
        }

        protected override void ConfigureApplicationContainer(ILifetimeScope existingContainer)
        {
            base.ConfigureApplicationContainer(existingContainer);

            existingContainer.Update(builder => {
                builder.RegisterInstance(this.loggerFactory).As<ILoggerFactory>();
                builder.RegisterModule<LoggingModule>();
                builder.RegisterModule<ServiceModule>();
            });
        }

        private static void Log(Exception ex, ILogger log)
        {
            if (ex is AggregateException)
            {
                foreach (var inner in ((AggregateException)ex).InnerExceptions)
                {
                    Log(inner, log);
                }
            }
            else
            {
                log.LogError(ex.Message, ex);
            }
        }
    }
}