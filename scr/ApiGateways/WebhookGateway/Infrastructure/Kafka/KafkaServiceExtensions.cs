using BuildingBlocks.Messaging.Kafka.Options;

namespace WebhookGateway.Infrastructure.Kafka
{
    public static class KafkaServiceExtensions
    {
       public static IServiceCollection AddKafkaProducer
            (this IServiceCollection services, IConfiguration configuration)
       {
            services
                .AddOptions<KafkaOptions>()
                .Bind(configuration.GetSection(KafkaOptions.SectionName))
                .ValidateDataAnnotations()
                .ValidateOnStart();

            return services; 
       }
    }
}
