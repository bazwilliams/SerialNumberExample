namespace SerialNumber.Service.Modules
{
    using Nancy;

    public sealed class HealthCheckModule : NancyModule
    {
        public HealthCheckModule()
        {
            this.Get("/healthcheck", _ => 200);
        }
    }
}