namespace OrchestratorAgent.Application.Contracts.Persistence
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(); 
    }
}
