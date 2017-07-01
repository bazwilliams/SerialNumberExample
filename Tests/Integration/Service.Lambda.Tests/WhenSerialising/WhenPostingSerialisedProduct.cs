namespace SerialNumber.Service.Lambda.Tests.WhenSerialising
{
    using FluentAssertions;

    using System.IO;
    using System.Threading.Tasks;

    using NSubstitute;

    using NUnit.Framework;

    using SerialNumber.Resources;

    using SerialNumber.Service.Lambda;

    [TestFixture]
    public class WhenPostingSerialisedProduct : ContextBase
    {
        private Stream response;

        private static string ProductName => "Sondek LP12";

        [SetUp]
        public async Task EstablishContext()
        {
            var resource = new CreateSerialisedProductResource { ProductName = ProductName };

            var inputStream = Utils.ToJsonMemoryStream(resource);

            this.response = await this.Handlers.SerialisedProductHandler(inputStream, this.LambdaContext);
        }

        [Test]
        public void ShouldReturnCorrectResource()
        {
            var resource = Utils.Bind<SerialisedProductResource>(this.response);
            resource.ProductName.Should().Be(ProductName);
            resource.SerialNumber.Should().ContainInOrder(this.ExpectedSerialNumber);
        }

        [Test]
        public void ShouldLogSuccess()
        {
            this.LambdaLogger.Received().LogLine("Success");
        }
    }
}
