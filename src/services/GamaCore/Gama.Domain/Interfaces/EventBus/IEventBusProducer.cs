namespace Gama.Domain.Interfaces.EventBus
{
    public interface IEventBusProducer
    {
        Task Publish<T>(T message, string routingKey) where T : class;
    }
}
