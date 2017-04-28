namespace SerialNumber.Persistence.Tables
{
    using Amazon.DynamoDBv2;
    using Amazon.DynamoDBv2.Model;

    using SerialNumber.Domain;
    using SerialNumber.Domain.Providers;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class SerialisedProductTable : ISerialisedProductWriter
    {
        private readonly IAmazonDynamoDB client;

        private readonly string tableName;

        public SerialisedProductTable(IAmazonDynamoDB client, string tableName)
        {
            this.client = client;
            this.tableName = tableName;
        }

        public async Task Write(SerialisedProduct value, CancellationToken ct)
        {
            var request = new PutItemRequest()
            {
                TableName = this.tableName,        
                Item = new Dictionary<string, AttributeValue>
                    {
                        { "Id", new AttributeValue { S = Guid.NewGuid().ToString() } },
                        { "ProductName", new AttributeValue { S = value.ProductName } },
                        { "SerialNumber", new AttributeValue { NS = value.SerialNumber.Select(x => x.ToString()).ToList() } }
                    }
            };

            await this.client.PutItemAsync(request, ct);
        }
    }
}