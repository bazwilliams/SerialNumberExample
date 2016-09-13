namespace SerialNumber.Service.App
{
    using Autofac;

    using Nancy;
    using Nancy.Bootstrapper;
    using Nancy.Bootstrappers.Autofac;

    using SerialNumber.Service.App.Ioc;

    public class Bootstrapper : AutofacNancyBootstrapper
    {
        protected override void RequestStartup(ILifetimeScope container, IPipelines pipelines, NancyContext context)
        {
            base.RequestStartup(container, pipelines, context);

            pipelines.AfterRequest += ctx => ctx.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        }

        protected override void ConfigureApplicationContainer(ILifetimeScope existingContainer)
        {
//            JsonSettings.MaxJsonLength = int.MaxValue;
//            JsonSettings.RetainCasing = true;

            base.ConfigureApplicationContainer(existingContainer);

            existingContainer.Update(builder => builder.RegisterModule<ServiceModule>());
        }
    }
}