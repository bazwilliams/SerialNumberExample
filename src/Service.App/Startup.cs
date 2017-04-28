namespace SerialNumber.Service.App
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Logging;

    using Nancy.Owin;

    public class Startup
    {
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            app.UseOwin(x => x.UseNancy(options => options.Bootstrapper = new Bootstrapper(loggerFactory)));
        }
    }
}