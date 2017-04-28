namespace Domain.Tests.SerialisedProductsSpecs
{
    using FluentAssertions;

    using NSubstitute;

    using NUnit.Framework;

    using System.Threading;

    [TestFixture]
    public class WhenSerialising : ContextBase
    {
        private static int ExpectedSerialNumber => 42;

        [SetUp]
        public void EstablishContext()
        {
            this.SerialNumberFactory.Create(1, Arg.Any<CancellationToken>()).Returns(new[] { ExpectedSerialNumber });
            this.Sut.GenerateSerialNumber(this.SerialNumberFactory, CancellationToken.None);
        }

        [Test]
        public void ShouldSetSerialNumber()
        {
            this.Sut.SerialNumber.Should().ContainInOrder(ExpectedSerialNumber);
        }
    }
}
