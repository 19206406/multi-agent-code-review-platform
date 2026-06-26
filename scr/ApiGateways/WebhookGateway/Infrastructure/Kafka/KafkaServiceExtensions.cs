using Microsoft.Extensions.Options;
using WebhookGateway.Infrastructure.Kafka.Services;

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

            services.AddSingleton(sp =>
            {
                var options = sp.GetRequiredService<IOptions<KafkaOptions>>().Value;
                return KafkaProducerFactory.Create(options);
            });

            return services; 
       }
    }
}
