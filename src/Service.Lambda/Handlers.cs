namespace SerialNumber.Service.Lambda
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    using SerialNumber.Service.Extensions;
    using SerialNumber.Service.Lambda.Ioc;
    using SerialNumber.Resources;
    using SerialNumber.Domain.Factories;
    
    using Amazon.Lambda.Core;

    using Autofac;

    public class Handlers
    {
        public async Task<Stream> SerialisedProductHandler(Stream inputStream, ILambdaContext context)
        {
            context.Logger.Log("Loading");

            using (var scope = ContainerFactory.Create(context.Logger).BeginLifetimeScope())
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