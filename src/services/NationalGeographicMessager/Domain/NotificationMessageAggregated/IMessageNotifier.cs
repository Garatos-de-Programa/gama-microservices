namespace NationalGeographicMessager.Domain.NotificationMessageAggregated
{
    public interface IMessageNotifier
    {
        Task NotifyAsync(IMessage message);
    }
}
