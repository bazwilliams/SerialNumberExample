﻿namespace SerialNumber.Domain.Facade.Tests.SerialisedProductFactorySpecs
{
    using FluentAssertions;

    using NUnit.Framework;

    using SerialNumber.Domain;
    using SerialNumber.Resources;

    [TestFixture]
    public class WhenCreating : ContextBase
    {
        private CreateSerialisedProductResource resource;

        private SerialisedProduct result;

        [SetUp]
        public void EstablishContext()
        {
            this.resource = new CreateSerialisedProductResource { ProductName = "Test" };
            this.result = this.Sut.Create(this.resource);
        }

        [Test]
        public void ShouldSetProductName()
        {
            this.result.ProductName.Should().Be(this.resource.ProductName);
        }

        [Test]
        public void ShouldNotSetSerialNumber()
        {
            this.result.SerialNumber.Should().BeEmpty();
        }

        [Test]
        public void ShouldHaveDefaultSerialNumberType()
        {
            this.result.SerialNumberType.Should().BeOfType<DefaultSerialNumberType>();
        }
    }
}
