namespace Auth.EventBus
{
    public class EventBusHandler : IEventBusHanlder
    {

        private readonly Dictionary<Type, List<Delegate>> _handlers = new();
        private object _lockObj = new object();

        public void Publish<T>(T @event) where T : IEvent
        {
            lock (_lockObj)
            {
                var eventType = typeof(T);
                if (_handlers.Any(x => x.Key == eventType))
                { 
                    foreach (var handler in _handlers[eventType])
                    {
                        handler?.DynamicInvoke(@event); 
                    }
                }
            }
             
        }

        public void Subscribe<T>(Action<T> handler) where T : IEvent
        {
            lock (_lockObj)
            {
                var eventType = typeof(T);

                if (!_handlers.Any(x => x.Key == eventType))
                {
                    _handlers.Add(eventType, new List<Delegate>()); 
                }
                 
                _handlers[eventType].Add(handler);
            }

        } 
    }
}
