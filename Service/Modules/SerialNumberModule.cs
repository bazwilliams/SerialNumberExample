namespace SerialNumber.Service.Modules
{
    using Nancy;
    using Nancy.ModelBinding;

    using SerialNumber.Domain.Factories;
    using SerialNumber.Resources;
    using SerialNumber.Service.Extensions;

    public class SerialNumberModule : NancyModule
    {
        private readonly ISerialNumberFactory serialNumberFactory;

        private readonly ISerialisedProductFactory<CreateSerialisedProductResource> serialisedProductFactory;

        public SerialNumberModule(ISerialNumberFactory serialNumberFactory, ISerialisedProductFactory<CreateSerialisedProductResource> serialisedProductFactory)
        {
            this.serialNumberFactory = serialNumberFactory;
            this.serialisedProductFactory = serialisedProductFactory;
            this.Post["/serial-numbers/"] = this.GenerateSerialNumber;
        }

        public dynamic GenerateSerialNumber(dynamic parameters)
        {
            var resource = this.Bind<CreateSerialisedProductResource>();
            var serialisedProduct = this.serialisedProductFactory.Create(resource);

            serialisedProduct.GenerateSerialNumber(this.serialNumberFactory);

            return this.Negotiate
                .WithStatusCode(HttpStatusCode.Created)
                .WithModel(serialisedProduct.ToResource());
        }
    }
}
