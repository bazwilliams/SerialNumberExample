namespace SerialNumber.Domain.Facade
{
    using SerialNumber.Domain.Factories;

    public class SerialNumberFactory : ISerialNumberFactory
    {
        private int nextVal;

        public SerialNumberFactory(int seed)
        {
            this.nextVal = seed;
        }

        public int Create()
        {
            return this.nextVal++;
        }
    }
}
