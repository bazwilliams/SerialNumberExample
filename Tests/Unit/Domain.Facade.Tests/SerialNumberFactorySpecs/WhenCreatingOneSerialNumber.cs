namespace SerialNumber.Domain.Facade.Tests.SerialNumberFactorySpecs
{
    using System.Collections.Generic;
    using System.Threading;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class WhenCreatingOneSerialNumber : ContextBase
    {
        private IEnumerable<int> result;

        [SetUp]
        public void EstablishContext()
        {
            this.result = this.Sut.Create(1, CancellationToken.None).Result;
        }

        [Test]
        public void ShouldGiveBackSeededSerialNumber()
        {
            this.result.Should().ContainInOrder(this.SerialNumberSeed);
        }
    }
}
