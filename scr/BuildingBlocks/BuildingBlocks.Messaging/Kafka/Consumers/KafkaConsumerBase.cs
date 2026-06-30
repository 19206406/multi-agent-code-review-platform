using BuildingBlocks.Messaging.Kafka.Options;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace BuildingBlocks.Messaging.Kafka.Consumers
{
    public abstract class KafkaConsumerBase<TKey, TValue> : BackgroundService
    {
        private readonly IConsumer<TKey, string> _consumer;
        private readonly ILogger _logger;
        private readonly KafkaOptions _options;
        protected abstract string TopicName { get; }

        protected KafkaConsumerBase(IOptions<KafkaOptions> options, ILogger logger)
        {
            _options = options.Value;
            _logger = logger;

            var config = new ConsumerConfig
            {
                BootstrapServers = options.Value.BootstrapServers,
                GroupId = options.Value.ConsumerGroupId,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = false,
                SessionTimeoutMs = 30000,
                MaxPollIntervalMs = 300000
            };

            _consumer = new ConsumerBuilder<TKey, string>(config).Build(); 
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _consumer.Subscribe(TopicName);

            _logger.LogInformation("Consumer '{ConsumerGroup}' subscribed to topic '{Topic}", _options.ConsumerGroupId, TopicName);

            while (!stoppingToken.IsCancellationRequested)
            {

                ConsumeResult<TKey, string>? result = null; 

                try
                {
                    result = _consumer.Consume(TimeSpan.FromMilliseconds(_options.ConsumeTimeoutMs));

                    if (result is null) continue;

                    _logger.LogDebug("Message received from { Topic} / Partition { Partition} / Offset { Offset}", result.Topic, result.Partition.Value, result.Offset.Value);

                    var message = JsonSerializer.Deserialize<TValue>(result.Message.Value)
                        ?? throw new InvalidOperationException($"The topic message could not be deserialized {TopicName}");

                    await HandleMessageAsync(message, result.Message.Headers, stoppingToken);

                    _consumer.Commit(result); 
                }
                catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
                {
                    _logger.LogInformation("Consumer '{ConsumerGroup}' arrested for shutdown", _options.ConsumerGroupId);

                    break; 
                }
                catch (ConsumeException ex)
                {
                    _logger.LogError(ex, "Error consuming a message from topic {Topi}", TopicName);

                    await Task.Delay(1000, stoppingToken); 
                }
                catch (Exception ex) when (result is not null)
                {
                    _logger.LogError(ex, "Error processing message from {Topic} / Offset {Offset}. The message will be retried.",
                        result.Topic, result.Offset.Value);

                    await Task.Delay(1000, stoppingToken); 
                }
            }

            _consumer.Close(); 
        }

        protected abstract Task HandleMessageAsync(TValue message, Headers headers, CancellationToken cancellationToken);

        public override void Dispose()
        {
            _consumer.Dispose(); 
            base.Dispose();
        }
    }
}
