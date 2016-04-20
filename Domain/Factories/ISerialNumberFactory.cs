namespace SerialNumber.Domain.Factories
{
    using System.Collections.Generic;

    public interface ISerialNumberFactory
    {
        IEnumerable<int> Create(int numberRequired);
    }
}
