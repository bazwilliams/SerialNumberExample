namespace SerialNumber.Service.Tests.WhenSerialising
{
    using FluentAssertions;

    using Nancy;
    using Nancy.Testing;

    using System.Threading.Tasks;

    using NUnit.Framework;

    using SerialNumber.Resources;

    [TestFixture]
    public class WhenPostingSerialisedProduct : ContextBase
    {
        private BrowserResponse response;

        private static string ProductName => "Sondek LP12";

        [SetUp]
        public async Task EstablishContext()
        {
            var resource = new CreateSerialisedProductResource { productName = ProductName };
            this.response = await this.Browser.Post(
                "/serial-numbers",
                with =>
                    {
                        with.Accept("application/json");
                        with.JsonBody(resource);
                    });
        }

        [Test]
        public void ShouldReceiveCreated()
        {
            this.response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Test]
        public void ShouldReturnCorrectResource()
        {
            var resource = this.response.Body.DeserializeJson<SerialisedProductResource>();
            resource.productName.Should().Be(ProductName);
            resource.serialNumber.Should().ContainInOrder(this.ExpectedSerialNumber);
        }
    }
}
