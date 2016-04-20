namespace Domain.Facade.Tests.SerialNumberFactorySpecs
{
    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class WhenCreating : ContextBase
    {
        private int result;

        [SetUp]
        public void EstablishContext()
        {
            this.result = this.Sut.Create();
        }

        [Test]
        public void ShouldGiveBackSeededSerialNumber()
        {
            this.result.Should().Be(this.SerialNumberSeed);
        }
    }
}
