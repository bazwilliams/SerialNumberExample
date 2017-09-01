namespace SerialNumber.Service.Ioc
{
    using System.Collections.Generic;

    using Amazon.DynamoDBv2;

    using Autofac;

    using Microsoft.Extensions.Configuration;

    using SerialNumber.Domain.Facade;
    using SerialNumber.Domain.Factories;
    using SerialNumber.Persistence.Tables;
    using SerialNumber.Resources;

    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddInMemoryCollection(new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("serialNumberSeed", "42") });
            configurationBuilder.AddEnvironmentVariables();

            builder.RegisterInstance(configurationBuilder.Build())
                .As<IConfiguration>()
                .SingleInstance();

            var client = new AmazonDynamoDBClient();
 
            builder.Register(ctx => new SequencesTable(client, ctx.Resolve<IConfiguration>()["sequencesTableName"]))
                .As<ISerialNumberFactory>()
                .SingleInstance();

            builder.RegisterType<SerialisedProductFactory>()
                .As<ISerialisedProductFactory<CreateSerialisedProductResource>>()
                .SingleInstance();
        }
    }
}