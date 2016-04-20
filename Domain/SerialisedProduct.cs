namespace SerialNumber.Domain
{
    using SerialNumber.Domain.Factories;

    public class SerialisedProduct
    {
        public int? SerialNumber { get; set; }
        
        public string ProductName { get; set; }

        public void GenerateSerialNumber(ISerialNumberFactory factory)
        {
            this.SerialNumber = factory.Create();
        }
    }
}
