namespace SerialNumber.Service.App.Ioc
{
    using System.Collections.Generic;

    using Autofac;

    using Amazon.DynamoDBv2;

    using Microsoft.Extensions.Configuration;

    using SerialNumber.Domain.Facade;
    
    using SerialNumber.Domain.Factories;

    using SerialNumber.Resources;

    using SerialNumber.Persistence.Tables;

    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddInMemoryCollection(new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("serialNumberSeed", "42") });
            configurationBuilder.AddEnvironmentVariables();
            var connectionStringConfig = configurationBuilder.Build();

            var client = new AmazonDynamoDBClient();
 
            builder.Register(ctx => new SerialNumberSequenceTable(client, ctx.Resolve<IConfiguration>()["deviceDetailsDb"]))
                .As<ISerialNumberFactory>()
                .SingleInstance();

            builder.RegisterType<SerialisedProductFactory>()
                .As<ISerialisedProductFactory<CreateSerialisedProductResource>>()
                .SingleInstance();
        }
    }
}