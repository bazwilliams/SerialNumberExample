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

        private IContainer GetContainer(ILambdaContext context) {
            if (this.container == null) {
                this.container = new LambdaContainerFactory(context.Logger).Create();
            }

            return this.container;
        }

        public async Task<Stream> SerialisedProductHandler(Stream inputStream, ILambdaContext context)
        {
            context.Logger.Log("Loading");

            using (var scope = this.GetContainer(context).BeginLifetimeScope())
            {
                var serialisedProductFactory = scope.Resolve<ISerialisedProductFactory<CreateSerialisedProductResource>>();
                
                var serialNumberFactory = scope.Resolve<ISerialNumberFactory>();
                
                context.Logger.Log("Loaded");

                try
                {
                    var resource = Utils.Bind<CreateSerialisedProductResource>(inputStream);

                    var serialisedProduct = serialisedProductFactory.Create(resource);

                    await serialisedProduct.GenerateSerialNumber(serialNumberFactory, CancellationToken.None);

                    var response = Utils.ToJsonMemoryStream(serialisedProduct.ToResource());

                    context.Logger.Log("Success");

                    return response;
                }
                catch (Exception e)
                {
                    context.Logger.Log(e.Message);
                    throw;
                }
            }
        }
    }
}