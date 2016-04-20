namespace SerialNumber.Service.App.Ioc
{
    using System.Configuration;

    using Ninject.Modules;

    using SerialNumber.Domain.Facade;
    using SerialNumber.Domain.Factories;
    using SerialNumber.Resources;

    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<ISerialNumberFactory>()
                .To<SerialNumberFactory>()
                .InSingletonScope()
                .WithConstructorArgument("seed", int.Parse(ConfigurationManager.AppSettings["SerialNumberSeed"]));

            this.Bind<ISerialisedProductFactory<CreateSerialisedProductResource>>().To<SerialisedProductFactory>();
        }
    }
}
