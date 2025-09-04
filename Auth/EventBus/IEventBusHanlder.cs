namespace Auth.EventBus
{
    public interface IEventBusHanlder
    { 
        void Publish<T>(T @event) where T : IEvent;
        void Subscribe<T>(Action<T> handler) where T : IEvent;
    }
}
