namespace SerialNumber.Service.Lambda.Console
{
    using SerialNumber.Resources;

    using SerialNumber.Service.Lambda;

    public class Program
    {
        public static void Main(string[] args)
        {
            var resource = new CreateSerialisedProductResource {
                   ProductName = "LP12",
                   ProductType = "Turntable"
            };

            var inputStream = Utils.ToJsonMemoryStream(resource);

            var lambdaContext = new LocalLambdaContext(new ConsoleLambdaLogger());

            var handlers = new Handlers();

            var stream = handlers.SerialisedProductHandler(inputStream, lambdaContext).Result;

            var response = Utils.Bind<SerialisedProductResource>(stream);
        }
    }
}
