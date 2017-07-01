namespace SerialNumber.Service.Lambda
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    using SerialNumber.Service.Extensions;
    using SerialNumber.Service.Ioc;
    using SerialNumber.Service.Lambda.Ioc;
    using SerialNumber.Resources;
    using SerialNumber.Domain.Factories;
    
    using Amazon.Lambda.Core;

    using Autofac;

    public class Handlers
    {
        private IContainer container;

        public Handlers()
        {
        }

        public Handlers(IContainer container)
        {
            this.container = container;
        }

        public async Task<Stream> SerialisedProductHandler(Stream inputStream, ILambdaContext context)
        {
            context.Logger.LogLine("Loading");

            using (var scope = this.GetContainer(context).BeginLifetimeScope())
            {
                var serialisedProductFactory = scope.Resolve<ISerialisedProductFactory<CreateSerialisedProductResource>>();
                
                var serialNumberFactory = scope.Resolve<ISerialNumberFactory>();
                
                context.Logger.LogLine("Loaded");

                try
                {
                    var resource = Utils.Bind<CreateSerialisedProductResource>(inputStream);

                    var serialisedProduct = serialisedProductFactory.Create(resource);

                    await serialisedProduct.GenerateSerialNumber(serialNumberFactory, CancellationToken.None);

                    var response = Utils.ToJsonMemoryStream(serialisedProduct.ToResource());

                    context.Logger.LogLine("Success");

                    return response;
                }
                catch (Exception e)
                {
                    context.Logger.LogLine(e.Message);
                    throw;
                }
            }
        }

        private IContainer GetContainer(ILambdaContext context) {
            if (this.container == null) {
                this.container = LambdaContainerFactory.Create(context.Logger);
            }

            return this.container;
        }
    }
}