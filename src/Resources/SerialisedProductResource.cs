namespace SerialNumber.Resources
{
    using System.Collections.Generic;

    public class SerialisedProductResource
    {
        public IEnumerable<int> SerialNumber { get; set; }

        public string ProductName { get; set; }
    }
}
