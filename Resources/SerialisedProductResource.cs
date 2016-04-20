namespace SerialNumber.Resources
{
    using System.Collections.Generic;

    public class SerialisedProductResource
    {
        public IEnumerable<int> serialNumber { get; set; }

        public string productName { get; set; }
    }
}
