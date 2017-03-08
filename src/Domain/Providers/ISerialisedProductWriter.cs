namespace SerialNumber.Domain.Providers 
{
    using System.Threading;
    using System.Threading.Tasks;

    using SerialNumber.Domain;
    
    public interface ISerialisedProductWriter
    {
        Task Write(SerialisedProduct product, CancellationToken ct);
    }
}