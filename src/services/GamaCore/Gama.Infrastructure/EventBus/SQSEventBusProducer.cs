using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;
using Gama.Domain.Interfaces.EventBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Gama.Infrastructure.EventBus
{
    internal class SQSEventBusProducer : IEventBusProducer
    {
        private readonly AmazonSQSClient _queueClient;
        private readonly string _queueURL;

        public SQSEventBusProducer(IConfiguration configuration)
        {
            _queueURL = configuration.GetConnectionString("EventBusConnectionString") ?? string.Empty;
            _queueClient = new AmazonSQSClient(RegionEndpoint.USEast2);
        }

        public async Task Publish<T>(T message, string routingKey) where T : class
        {
            var request = new SendMessageRequest
            {
                QueueUrl = _queueURL,
                MessageBody = JsonConvert.SerializeObject(message),
                MessageGroupId = Guid.NewGuid().ToString(),
            };

            await _queueClient.SendMessageAsync(request);
        }
    }
}
