namespace BuildingBlocks.Messaging.Kafka.Options
{
    public sealed class KafkaOptions
    {
        public const string SectionName = "Kafka";

        public string BootstrapServers { get; init; } = string.Empty;
        public string ConsumerGroupId { get; init; } = string.Empty;
        public int ConsumeTimeoutMs { get; init; } = 1000;
        public int MaxRetryAttemps { get; init; } = 3; 
    }
}
