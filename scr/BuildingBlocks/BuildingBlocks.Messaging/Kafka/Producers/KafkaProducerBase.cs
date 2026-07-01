using BuildingBlocks.Messaging.Kafka.Options;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace BuildingBlocks.Messaging.Kafka.Producers
{
    public abstract class KafkaProducerBase<TKey, TValue> : IAsyncDisposable
    {
        private readonly IProducer<TKey, Null> _innerProducer;
        protected readonly IProducer<TKey, string> Producer;
        protected readonly ILogger _logger;
        private bool _disposed; 

        protected KafkaProducerBase(IOptions<KafkaOptions> options, ILogger logger)
        {
            _logger = logger;

            var config = new ProducerConfig
            {
                BootstrapServers = options.Value.BootstrapServers,
                Acks = Acks.All,
                EnableIdempotence = true,
                MessageSendMaxRetries = 5,
                RetryBackoffMs = 100,
                LingerMs = 5,
            };

            Producer = new ProducerBuilder<TKey, string>(config).Build(); 
        }

        protected async Task ProduceAsync
            (string topic, TKey key, TValue value, Headers? headers = null, CancellationToken cancellationToken = default)
        {
            var serializedValue = JsonSerializer.Serialize(value);

            var message = new Message<TKey, string>
            {
                Key = key,
                Value = serializedValue,
                Headers = headers ?? []
            }; 

            try
            {
                var result = await Producer.ProduceAsync(topic, message, cancellationToken);

                _logger.LogDebug("Message generated in {Topic} / Partition {Partition} / Offset {Offset}",
                    result.Topic, result.Partition, result.Offset); 
            }
            catch (ProduceException<TKey, string> ex)
            {
                _logger.LogError(ex, "Error generating a message in topic {Topic}. Error: {ErrorCode} — {Reason}",
                    topic, ex.Error.Code, ex.Error.Reason);

                throw; 
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_disposed) return;
            _disposed = true;

            Producer.Flush(TimeSpan.FromSeconds(10));
            Producer.Dispose();

            await ValueTask.CompletedTask; 
        }
    }
}
