using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrchestratorAgent.Application.Contracts.Persistence;
using OrchestratorAgent.Persistence.Context;
using OrchestratorAgent.Persistence.Repositories;

namespace OrchestratorAgent.Persistence
{
    public static class PersistenceServicesRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<OrchestratorAgentDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("OrchestratorAgentConnectionString")));


            // repositories 
            services.AddScoped<IUnitOfWork, UnitOfWork>(); 
            services.AddScoped<IPipelineRunRepository, PipelineRunRepository>();
            //services.AddScoped<IAgentTaskStateRepository, AgentTaskStateRepository>();  

            return services; 

        }
    }
}
