namespace SerialNumber.Domain.Factories
{
    using System.Collections.Generic;
    
    using System.Threading;
    using System.Threading.Tasks;

    public interface ISerialNumberFactory
    {
        Task<IEnumerable<int>> Create(int numberRequired, CancellationToken ct);
    }
}
