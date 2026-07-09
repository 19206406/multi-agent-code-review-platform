namespace OrchestratorAgent.Domain.Entities
{
    public class PipelineRun
    {
        public Guid Id { get; set; }
        public Guid CorrelationId { get; set; }
        public string Status { get; set; } = string.Empty;
        public string RepositoryFullName { get; set; } = string.Empty;
        public int PrNumber { get; set; }
        public string PrTitle { get; set; } = string.Empty;
        public string PrAuthor { get; set; } = string.Empty;
        public string HeadSha { get; set; } = string.Empty;
        public string BaseSha { get; set; } = string.Empty;
        public string HeadBranch { get; set; } = string.Empty;
        public string BaseBranch { get; set; } = string.Empty;
        public string GithubDeliveryId { get; set; } = string.Empty; 
        public int RetryCount { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public string EnrichedDiff { get; set; } = string.Empty;
        public string ChangedFiles { get; set; } = string.Empty;
        public string RagContext { get; set; } = string.Empty;
        public DateTimeOffset StartedAt { get; set; } 
        public DateTimeOffset EnrichedAt { get; set; }
        public DateTimeOffset AgentsDispatchedAt { get; set; }
        public DateTimeOffset CompletedAt { get; set; }
        public DateTimeOffset FailedAt { get; set; }
        public int GithubCommentId { get; set; }
        public string GithubCommentUrl { get; set; } = string.Empty;

        // relationship 
        public ICollection<AgentTaskState> AgentTaskStates { get; set; } = new List<AgentTaskState>(); 
    }
}
