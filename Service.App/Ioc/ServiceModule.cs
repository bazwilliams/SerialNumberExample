namespace SerialNumber.Service.App.Ioc
{
    using System.Configuration;

    using Autofac;

    using SerialNumber.Domain.Facade;
    using SerialNumber.Domain.Factories;
    using SerialNumber.Resources;

    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(
                new SerialNumberFactory(int.Parse(ConfigurationManager.AppSettings["SerialNumberSeed"])))
                .As<ISerialNumberFactory>()
                .SingleInstance();

            builder.RegisterType<SerialisedProductFactory>().As<ISerialisedProductFactory<CreateSerialisedProductResource>>().SingleInstance();
        }
    }
}
