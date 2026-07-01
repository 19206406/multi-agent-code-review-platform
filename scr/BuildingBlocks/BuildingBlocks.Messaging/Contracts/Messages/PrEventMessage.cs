namespace BuildingBlocks.Messaging.Contracts.Messages
{
    public sealed record PrEventMessage
    (
        Guid EventId,
        DateTimeOffset ReceivedAt,
        string GitHubDeliveryId,
        string Action,
        RepositoryInfo Repository,
        PullRequestInfo PullRequest,
        string RawPayload
    ); 

    public sealed class RepositoryInfo
    {
        public string FullName { get; init; } = string.Empty;          // "org/repo" — también es la partition key
        public string DefaultBranch { get; init; } = string.Empty;
        public string Language { get; init; } = string.Empty;
        public string CloneUrl { get; init; } = string.Empty;
    }

    public sealed class PullRequestInfo
    {
        public int Number { get; init; }
        public string Title { get; init; } = string.Empty;
        public string HeadSha { get; init; } = string.Empty;
        public string BaseSha { get; init; } = string.Empty;
        public string HeadBranch { get; init; } = string.Empty;
        public string BaseBranch { get; init; } = string.Empty;
        public string Author { get; init; } = string.Empty;
        public string DiffUrl { get; init; } = string.Empty;
    }
}
