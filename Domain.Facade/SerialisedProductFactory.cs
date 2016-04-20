namespace SerialNumber.Domain.Facade
{
    using SerialNumber.Domain.Factories;
    using SerialNumber.Resources;

    public class SerialisedProductFactory : ISerialisedProductFactory<CreateSerialisedProductResource>
    {
        public SerialisedProduct Create(CreateSerialisedProductResource value)
        {
            return new SerialisedProduct { ProductName = value.productName };
        }
    }
}
