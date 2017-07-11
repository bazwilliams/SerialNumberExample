namespace SerialNumber.Service.Lambda.Tests.WhenSerialising
{
    using Amazon.Lambda.Core;

    using Autofac;

    using NUnit.Framework;

    using NSubstitute;

    using SerialNumber.Domain.Factories;

    using SerialNumber.Domain.Facade;

    using SerialNumber.Resources;

    using SerialNumber.Service.Lambda;

    [TestFixture]
    public class ContextBase
    {
        protected Handlers Handlers { get; private set; }

        protected int ExpectedSerialNumber => 123456;

        protected ILambdaContext LambdaContext { get; private set; }

        protected ILambdaLogger LambdaLogger { get; private set; }

        [SetUp]
        public void CommonContext()
        {
            var serialisedProductFactory = new SerialisedProductFactory();
            var serialNumberFactory = new SerialNumberFactory(this.ExpectedSerialNumber);

            var builder = new ContainerBuilder();

            builder.RegisterInstance(serialNumberFactory)
                .As<ISerialNumberFactory>()
                .SingleInstance();

            builder.RegisterInstance(serialisedProductFactory)
                .As<ISerialisedProductFactory<CreateSerialisedProductResource>>()
                .SingleInstance();

            this.LambdaContext = Substitute.For<ILambdaContext>();

            this.LambdaLogger = Substitute.For<ILambdaLogger>();

            this.LambdaContext.Logger.Returns(this.LambdaLogger);

            this.Handlers = new Handlers(builder.Build());
        }
    }
}
