namespace SerialNumber.Domain.Facade
{
    using SerialNumber.Domain.Factories;
    using System.Collections.Generic;

    public class SerialNumberFactory : ISerialNumberFactory
    {
        private int nextVal;

        public SerialNumberFactory(int seed)
        {
            this.nextVal = seed;
        }

        public IEnumerable<int> Create(int numberRequired)
        {
            var serialNumbers = new List<int>();
            for (var i = 0; i < numberRequired; i++)
            {
                serialNumbers.Add(this.nextVal++);
            }

            return serialNumbers;
        }
    }
}
