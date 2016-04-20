namespace SerialNumber.Domain
{
    public class DefaultSerialNumberType : ISerialNumberType
    {
        public int NumberRequired()
        {
            return 1;
        }
    }
}