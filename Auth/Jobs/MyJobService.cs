using Auth.EventBus;

namespace Auth.Jobs
{
    public class MyJobService : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly ILogger<MyJobService> _logger;
        private readonly IEventBusHanlder _eventBus;

        public MyJobService(ILogger<MyJobService> logger, IEventBusHanlder eventBus)
        {
            _logger = logger;
            _eventBus = eventBus;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {

            _timer = new Timer(SubscribeNotifications, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
            return Task.CompletedTask;
        }

        private void SubscribeNotifications(object state)
        {
            var subscriberId = Guid.NewGuid();
            _eventBus.Subscribe<NotificationEvent>(x =>
            {
                _logger.LogInformation($"{subscriberId} Subscriber - Notification: {x.Message} Has Been Subscribed.");
            });

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
