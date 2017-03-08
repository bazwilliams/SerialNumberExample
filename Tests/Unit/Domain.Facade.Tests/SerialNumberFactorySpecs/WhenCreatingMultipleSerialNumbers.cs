namespace Domain.Facade.Tests.SerialNumberFactorySpecs
{
    using System.Collections.Generic;

    using System.Threading;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class WhenCreatingMultipleSerialNumbers : ContextBase
    {
        private IEnumerable<int> result;

        [SetUp]
        public void EstablishContext()
        {
            this.result = this.Sut.Create(5, CancellationToken.None).Result;
        }

        [Test]
        public void ShouldGiveBackSeededSerialNumber()
        {
            this.result.Should()
                .ContainInOrder(
                    this.SerialNumberSeed,
                    this.SerialNumberSeed + 1,
                    this.SerialNumberSeed + 2,
                    this.SerialNumberSeed + 3,
                    this.SerialNumberSeed + 4);
        }
    }
}
