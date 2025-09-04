namespace Auth.EventBus
{
    public class NotificationEvent : IEvent
    {
        public string Message { get; set; }
    }
}
