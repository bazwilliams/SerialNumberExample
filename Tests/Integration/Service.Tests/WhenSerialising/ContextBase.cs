namespace SerialNumber.Service.Tests.WhenSerialising
{
    using Nancy.Testing;

    using NUnit.Framework;

    using SerialNumber.Domain.Facade;
    using SerialNumber.Service.Modules;

    [TestFixture]
    public class ContextBase
    {
        protected Browser Browser { get; private set; }

        protected int ExpectedSerialNumber => 123456;

        [SetUp]
        public void CommonContext()
        {
            var serialisedProductFactory = new SerialisedProductFactory();
            var serialNumberFactory = new SerialNumberFactory(this.ExpectedSerialNumber);
             
            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                    {
                        with.Module<SerialNumberModule>();
                        with.Dependency(serialisedProductFactory);
                        with.Dependency(serialNumberFactory);
                    });
            this.Browser = new Browser(bootstrapper);
        }
    }
}
