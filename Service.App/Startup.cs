namespace SerialNumber.Service.App
{
    using Owin;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseNancy(
                options =>
                    {
                        options.Bootstrapper = new Bootstrapper();
                    });
        }
    }
}
