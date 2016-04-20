namespace SerialNumber.Service.App
{
    using Nancy;
    using Nancy.Bootstrapper;
    using Nancy.Bootstrappers.Ninject;
    using Nancy.Json;

    using Ninject;
    using Ninject.Modules;

    using SerialNumber.Service.App.Ioc;

    public class Bootstrapper : NinjectNancyBootstrapper
    {
        protected override void RequestStartup(IKernel container, IPipelines pipelines, NancyContext context)
        {
            base.RequestStartup(container, pipelines, context);

            pipelines.AfterRequest += ctx => ctx.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        }

        protected override void ConfigureApplicationContainer(IKernel existingContainer)
        {
            base.ConfigureApplicationContainer(existingContainer);

            existingContainer.Load(new INinjectModule[] { new ServiceModule() });

            JsonSettings.MaxJsonLength = int.MaxValue;
            JsonSettings.RetainCasing = true;
        }
    }
}