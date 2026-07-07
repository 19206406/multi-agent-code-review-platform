namespace BuildingBlocks.Messaging.Contracts.Messages
{
    public sealed record PrEventMessage
    (
        string EventId,
        DateTime ReceivedAt,
        string GitHubDeliveryId,
        string Action,
        Repository Repository,
        PullRequest PullRequest 
    );

    public record PullRequest(
        int Number,
        string Title,
        string State,
        string Url,
        string HtmlUrl,
        PullRequestUser User,
        PullRequestHead Head,
        PullRequestBase Base,
        string DiffUrl,
        DateTime CreatedAt,
        DateTime UpdatedAt);

    public record PullRequestUser(string Login);

    public record PullRequestHead(
        string Sha,
        string Ref);

    public record PullRequestBase(
        string Sha,
        string Ref);

    public record Repository(
        string FullName,
        string DefaultBranch,
        string Language,
        string CloneUrl,
        string HtmlUrl);
}
