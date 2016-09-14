namespace Domain.Facade.Tests.SerialNumberFactorySpecs
{
    using NUnit.Framework;

    using SerialNumber.Domain.Facade;

    [TestFixture]
    public class ContextBase
    {
        protected int SerialNumberSeed => 12334567;

        protected SerialNumberFactory Sut { get; private set; }

        [SetUp]
        public void CommonContext()
        {
            this.Sut = new SerialNumberFactory(this.SerialNumberSeed);
        }
    }
}
