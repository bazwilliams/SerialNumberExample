namespace SerialNumber.Domain.Factories
{
    public interface ISerialisedProductFactory<in T>
    {
        SerialisedProduct Create(T value);
    }
}
