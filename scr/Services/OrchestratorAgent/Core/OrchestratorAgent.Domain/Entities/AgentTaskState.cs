namespace OrchestratorAgent.Domain.Entities
{
    public class AgentTaskState
    {
        public Guid Id { get; set; }
        public Guid PipelineRunId { get; set; }
        public string AgentType { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty; 
        public DateTimeOffset DispatchedAt { get; set; }
        public DateTimeOffset CompletedAt { get; set; }
        public int RetryCount { get; set; }
        public string LastError { get; set; } = string.Empty;
        public bool ValidationPassed { get; set; }
        public string ValidationReason { get; set; } = string.Empty; 
    }
}
