using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using Gama.Domain.Interfaces.EventBus;

namespace Gama.Infrastructure.EventBus
{
    internal class RabbitMqEventBusProducer : IEventBusProducer
    {
        private readonly IModel _channel;
        private readonly IConnection _connection;
        private readonly object _lock = new();

        public RabbitMqEventBusProducer(IConfiguration configuration)
        {
            var connectionFactory = new ConnectionFactory()
            {
                TopologyRecoveryEnabled = true,
                Uri = new Uri(configuration.GetConnectionString("EventBusConnectionString") ?? string.Empty),
                AutomaticRecoveryEnabled = true,
            };

            _connection = connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(
                exchange: "gama.api:events-exchange",
                type: "topic",
                durable: true
                );
        }

        public Task Publish<T>(T message, string routingKey) where T : class
        {
            lock (_lock)
            {
                var messageString = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(messageString);
                _channel.BasicPublish(
                    exchange: "gama.api:events-exchange",
                    routingKey: routingKey,
                    body: body
                    );
            }

            return Task.CompletedTask;
        }
    }
}
