namespace SerialNumber.Service.Extensions
{
    using System.Linq;

    using SerialNumber.Domain;
    using SerialNumber.Resources;

    public static class SerialisedProductExtensions
    {
        public static SerialisedProductResource ToResource(this SerialisedProduct domain)
        {
            return domain.SerialNumber.Any()
                ? new SerialisedProductResource
                           {
                               ProductName = domain.ProductName,
                               SerialNumber = domain.SerialNumber
                           }
                : null;
        }
    }
}
