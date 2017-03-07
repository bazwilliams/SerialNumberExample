namespace SerialNumber.Domain
{
    using System.Collections.Generic;
    using System.Linq;

    using SerialNumber.Domain.Factories;

    public class SerialisedProduct
    {
        public SerialisedProduct()
        {
            this.SerialNumber = Enumerable.Empty<int>();
        }

        public IEnumerable<int> SerialNumber { get; set; }
        
        public string ProductName { get; set; }

        public ISerialNumberType SerialNumberType { get; set; }

        public void GenerateSerialNumber(ISerialNumberFactory factory)
        {
            this.SerialNumber = factory.Create(this.SerialNumberType.NumberRequired());
        }
    }
}
