namespace Domain.Facade.Tests.SerialNumberFactorySpecs
{
    using System.Collections.Generic;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class WhenCreatingOneSerialNumber : ContextBase
    {
        private IEnumerable<int> result;

        [SetUp]
        public void EstablishContext()
        {
            this.result = this.Sut.Create(1);
        }

        [Test]
        public void ShouldGiveBackSeededSerialNumber()
        {
            this.result.Should().ContainInOrder(this.SerialNumberSeed);
        }
    }
}
