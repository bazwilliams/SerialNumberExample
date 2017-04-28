namespace SerialNumber.Domain
{
    public class PairedSerialNumberType : ISerialNumberType
    {
        public int NumberRequired()
        {
            return 2;
        }
    }
}