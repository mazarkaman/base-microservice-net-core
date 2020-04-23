namespace PhungDKH.EventBusRabbitMQ
{
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using PhungDKH.EvenBus;
    using PhungDKH.EvenBus.Abstractions;
    using PhungDKH.EvenBus.Events;
    using Polly;
    using Polly.Retry;
    using RabbitMQ.Client;
    using RabbitMQ.Client.Exceptions;
    using System;
    using System.Net.Sockets;
    using System.Text;

    public class EventBusRabbitMQ : IEventBus
    {
        private readonly IRabbitMQPersistentConnection _persistentConnection;
        private readonly ILogger<EventBusRabbitMQ> _logger;
        private readonly IEventBusSubscriptionsManager _subsManager;

        private readonly string _exchange;
        private string _queueName;
        private readonly int _retryCount;

        public EventBusRabbitMQ(IRabbitMQPersistentConnection persistentConnection, ILogger<EventBusRabbitMQ> logger, IEventBusSubscriptionsManager subsManager, string exchange = null, string queueName = null, int retryCount = 5)
        {
            _persistentConnection = persistentConnection ?? throw new ArgumentNullException(nameof(persistentConnection));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _subsManager = subsManager ?? new InMemoryEventBusSubscriptionsManager();
            _exchange = exchange;
            _queueName = queueName;
            _subsManager.OnEventRemoved += SubsManager_OnEventRemoved;
            _retryCount = retryCount;
        }

        private void SubsManager_OnEventRemoved(object sender, string eventName)
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            using (var channel = _persistentConnection.CreateModel())
            {
                channel.QueueUnbind(queue: _queueName,
                    exchange: _exchange,
                    routingKey: eventName);

                if (_subsManager.IsEmpty)
                {
                    _queueName = string.Empty;
                }
            }
        }

        public void Publish(IntegrationEvent @event)
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            var policy = RetryPolicy.Handle<BrokerUnreachableException>()
                .Or<SocketException>()
                .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                {
                    _logger.LogWarning(ex, "Could not publish event: {EventId} after {Timeout}s ({ExceptionMessage})", @event.Id, $"{time.TotalSeconds:n1}", ex.Message);
                });

            var routingKey = @event.RoutingKey;

            _logger.LogInformation("Creating RabbitMQ channel to publish event: {EventId} ({EventName})", @event.Id, routingKey);

            using (var channel = _persistentConnection.CreateModel())
            {

                _logger.LogInformation("Declaring RabbitMQ exchange to publish event: {EventId}", @event.Id);

                channel.ExchangeDeclare(exchange: _exchange, ExchangeType.Direct);
                channel.QueueDeclare(_queueName, true, false, false, null);
                channel.QueueBind(_queueName, _exchange, routingKey);

                var message = JsonConvert.SerializeObject(@event);
                var body = Encoding.UTF8.GetBytes(message);

                _logger.LogInformation("Publishing event to RabbitMQ: {EventId}", @event.Id);

                policy.Execute(() =>
                {
                    var properties = channel.CreateBasicProperties();
                    properties.DeliveryMode = 2; // persistent

                    _logger.LogInformation("Publishing event to RabbitMQ: {EventId}", @event.Id);

                    channel.BasicPublish(
                        exchange: _exchange,
                        routingKey: routingKey,
                        mandatory: true,
                        basicProperties: properties,
                        body: body);
                });
            }
        }
    }
}
