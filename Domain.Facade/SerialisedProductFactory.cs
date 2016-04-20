﻿namespace SerialNumber.Domain.Facade
{
    using SerialNumber.Domain.Factories;
    using SerialNumber.Resources;

    public class SerialisedProductFactory : ISerialisedProductFactory<CreateSerialisedProductResource>
    {
        public SerialisedProduct Create(CreateSerialisedProductResource value)
        {
            return new SerialisedProduct
                       {
                           ProductName = value.productName,
                           SerialNumberType = SerialNumberType(value.productType)
                       };
        }

        private static ISerialNumberType SerialNumberType(string productType)
        {
            switch (productType)
            {
                case "speakers":
                    return new PairedSerialNumberType();
                default:
                    return new DefaultSerialNumberType();
            }
        }
    }
}
