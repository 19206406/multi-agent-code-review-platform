using BuildingBlocks.Messaging.Kafka.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Messaging.Kafka.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddKafkaInfratructure(this IServiceCollection services, IConfiguration configuration)
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
