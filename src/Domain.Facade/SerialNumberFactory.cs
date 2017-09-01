namespace SerialNumber.Domain.Facade
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using SerialNumber.Domain.Factories;

    public class SerialNumberFactory : ISerialNumberFactory
    {
        private int nextVal;

        public SerialNumberFactory(int seed)
        {
            this.nextVal = seed;
        }

        public async Task<IEnumerable<int>> Create(int numberRequired, CancellationToken ct)
        {
            var serialNumbers = new List<int>();
            for (var i = 0; i < numberRequired; i++)
            {
                serialNumbers.Add(this.nextVal++);
            }

            return await Task.FromResult(serialNumbers);
        }
    }
}
