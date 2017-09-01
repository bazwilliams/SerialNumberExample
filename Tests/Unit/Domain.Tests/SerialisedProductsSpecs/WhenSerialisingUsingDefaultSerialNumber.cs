namespace SerialNumber.Domain.Tests.SerialisedProductsSpecs
{
    using System.Threading;

    using FluentAssertions;

    using NSubstitute;

    using NUnit.Framework;

    [TestFixture]
    public class WhenSerialising : ContextBase
    {
        private static int ExpectedSerialNumber => 42;

        [SetUp]
        public void EstablishContext()
        {
            this.SerialNumberFactory.Create(1, Arg.Any<CancellationToken>()).Returns(new[] { ExpectedSerialNumber });
            this.Sut.GenerateSerialNumber(this.SerialNumberFactory, CancellationToken.None).Wait();
        }

        [Test]
        public void ShouldSetSerialNumber()
        {
            this.Sut.SerialNumber.Should().ContainInOrder(ExpectedSerialNumber);
        }
    }
}
