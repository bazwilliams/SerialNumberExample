namespace SerialNumber.Domain
{
    using System.Collections.Generic;
    using System.Linq;

    using System.Threading;
    using System.Threading.Tasks;

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

        public async Task GenerateSerialNumber(ISerialNumberFactory factory, CancellationToken ct)
        {
            this.SerialNumber = await factory.Create(this.SerialNumberType.NumberRequired(), ct);
        }
    }
}
