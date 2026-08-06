namespace OrchestratorAgent.Domain.Entities
{
    public class AgentTaskState
    {
        public Guid Id { get; set; }
        public Guid PipelineRunId { get; set; }
        public TypeAgentTask AgentType { get; set; }
        public StateAgentTask Status { get; set; } 
        public DateTimeOffset DispatchedAt { get; set; }
        public DateTimeOffset CompletedAt { get; set; }
        public int RetryCount { get; set; }
        public string LastError { get; set; } = string.Empty;
        public bool ValidationPassed { get; set; }
        public string ValidationReason { get; set; } = string.Empty;

        // relationship 
        public PipelineRun PipelineRun { get; set; } = null!; 
    }

    public enum StateAgentTask
    {
        Dispatched = 0, 
        Processing = 1, 
        Completed = 2, 
        Failed = 3, 
        Retrying = 4, 
    }

    public enum TypeAgentTask
    {
        Security = 0, 
        Architecture = 1, 
        Quality = 2, 
    }
}
