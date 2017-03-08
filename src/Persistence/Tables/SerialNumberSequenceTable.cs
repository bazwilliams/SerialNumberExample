﻿namespace SerialNumber.Persistence.Tables
{
    using Amazon.DynamoDBv2;
    using Amazon.DynamoDBv2.Model;

    using SerialNumber.Domain;
    using SerialNumber.Domain.Factories;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class SerialNumberSequenceTable : ISerialNumberFactory
    {
        private readonly IAmazonDynamoDB client;

        private readonly string tableName;

        public SerialNumberSequenceTable(IAmazonDynamoDB client, string tableName)
        {
            this.client = client;
            this.tableName = tableName;
        }

        public async Task<IEnumerable<int>> Create(int numberRequired, CancellationToken ct)
        {
            var getRequest = new GetItemRequest()
            {
                TableName = this.tableName,
                Key = new Dictionary<string, AttributeValue> { { "Sequence", new AttributeValue { S = "SerialNumber" } } }
            };

            var response = await this.client.GetItemAsync(getRequest, ct);

            var nextValue = int.Parse(response.Item["NextValue"].N);
            var newNextValue = nextValue + numberRequired;

            var putRequest = new PutItemRequest()
            {
                TableName = this.tableName,        
                Item = new Dictionary<string, AttributeValue>
                    {
                        { "Sequence", new AttributeValue { S = "SerialNumber" } },
                        { "NextValue", new AttributeValue { N = newNextValue.ToString() } }
                    }
            };

            await this.client.PutItemAsync(putRequest, ct);

            return Enumerable.Range(nextValue, numberRequired);
        }
    }
}