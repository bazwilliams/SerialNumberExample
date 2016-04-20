namespace SerialNumber.Service.Extensions
{
    using SerialNumber.Domain;
    using SerialNumber.Resources;

    public static class SerialisedProductExtensions
    {
        public static SerialisedProductResource ToResource(this SerialisedProduct domain)
        {
            return domain.SerialNumber.HasValue
                ? new SerialisedProductResource
                           {
                               productName = domain.ProductName,
                               serialNumber = domain.SerialNumber.Value
                           }
                : null;
        }
    }
}
