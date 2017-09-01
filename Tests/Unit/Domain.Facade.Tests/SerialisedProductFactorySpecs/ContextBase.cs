namespace SerialNumber.Domain.Facade.Tests.SerialisedProductFactorySpecs
{
    using NUnit.Framework;

    using SerialNumber.Domain.Facade;

    [TestFixture]
    public class ContextBase
    {
        protected SerialisedProductFactory Sut { get; private set; }

        [SetUp]
        public void CommonContext()
        {
            this.Sut = new SerialisedProductFactory();
        }
    }
}
