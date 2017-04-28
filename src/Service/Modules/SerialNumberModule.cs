namespace SerialNumber.Service.Modules
{
    using Nancy;
    using Nancy.ModelBinding;

    using SerialNumber.Domain.Factories;
    using SerialNumber.Resources;
    using SerialNumber.Service.Extensions;

    using System.Threading;
    using System.Threading.Tasks;

    public class SerialNumberModule : NancyModule
    {
        private readonly ISerialNumberFactory serialNumberFactory;

        private readonly ISerialisedProductFactory<CreateSerialisedProductResource> serialisedProductFactory;

        public SerialNumberModule(ISerialNumberFactory serialNumberFactory, ISerialisedProductFactory<CreateSerialisedProductResource> serialisedProductFactory)
        {
            this.serialNumberFactory = serialNumberFactory;
            this.serialisedProductFactory = serialisedProductFactory;
            this.Post("/serial-numbers/", async (args, ct) => await this.GenerateSerialNumber(ct));
        }

        public async Task<dynamic> GenerateSerialNumber(CancellationToken ct)
        {
            var resource = this.Bind<CreateSerialisedProductResource>();
            var serialisedProduct = this.serialisedProductFactory.Create(resource);

            await serialisedProduct.GenerateSerialNumber(this.serialNumberFactory, ct);

            return await this.Negotiate
                .WithStatusCode(HttpStatusCode.Created)
                .WithModel(serialisedProduct.ToResource());
        }
    }
}
