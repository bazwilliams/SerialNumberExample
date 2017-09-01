namespace SerialNumber.Domain.Tests.SerialisedProductsSpecs
{
    using NSubstitute;

    using NUnit.Framework;

    using SerialNumber.Domain;
    using SerialNumber.Domain.Factories;

    [TestFixture]
    public class ContextBase
    {
        protected SerialisedProduct Sut { get; private set; }

        protected ISerialNumberFactory SerialNumberFactory { get; private set; }

        [SetUp]
        public void CommonContext()
        {
            this.Sut = new SerialisedProduct { ProductName = "Sondek LP12", SerialNumberType = new DefaultSerialNumberType() };
            this.SerialNumberFactory = Substitute.For<ISerialNumberFactory>();
        }
    }
}
