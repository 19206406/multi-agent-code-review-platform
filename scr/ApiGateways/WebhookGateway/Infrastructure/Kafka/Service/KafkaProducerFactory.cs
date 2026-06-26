using Confluent.Kafka;

namespace WebhookGateway.Infrastructure.Kafka.Services
{
    public static class KafkaProducerFactory
    {

        public static IProducer<string, string> Create(KafkaOptions options)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = options.BootstrapServers, 
                Acks = Acks.All, 
                EnableIdempotence = true, 
                MessageSendMaxRetries = options.MessageSendMaxRetries, 
                MessageTimeoutMs = options.MessageTimeoutMs, 
                CompressionType = CompressionType.Snappy, 
                LingerMs = 0 
            };

            return new ProducerBuilder<string, string>(config)
                .SetKeySerializer(Serializers.Utf8)
                .SetValueSerializer(Serializers.Utf8)
                .SetErrorHandler((_, error) =>
                {
                    Console.Error.WriteLine($"[Kafka Producer Error] {error.Code}: {error.Reason} | IsFatal: {error.IsFatal}");
                })
                .Build(); 
        }
    }
}
