namespace WebhookGateway.Infrastructure.Kafka.Services
{
    public sealed class KafkaOptions
    {
        public const string SectionName = "Kafka";

        public string BootstrapServers { get; set; } = string.Empty;
        public string PrEventsRawTopic { get; set; } = "pr.events.raw";
        public int MessageTimeoutMs { get; set; } = 5000;
        public int MessageSendMaxRetries { get; set; } = 3; 
    }
}
