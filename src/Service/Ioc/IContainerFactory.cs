namespace SerialNumber.Service.Ioc
{
    using Autofac;

    public interface IContainerFactory
    {
        IContainer Create();
    }
}