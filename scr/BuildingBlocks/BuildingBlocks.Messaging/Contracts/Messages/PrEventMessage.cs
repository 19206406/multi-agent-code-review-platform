using System.Text.Json.Serialization;

namespace BuildingBlocks.Messaging.Contracts.Messages
{
    public sealed record PrEventMessage
    (
        string EventId,
        DateTimeOffset ReceivedAt,
        string GitHubDeliveryId,
        string Action,
        Repository Repository,
        PullRequest PullRequest,
        Sender Sender
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
        DateTimeOffset CreatedAt,
        DateTimeOffset UpdatedAt)
    {
        // No viene directo del JSON de GitHub; se deriva de User.Login
        [JsonIgnore]
        public string Author => User.Login;

        // Aplanados para conveniencia del consumidor (Orchestrator),
        // igual que en GithubPrPayload
        [JsonIgnore]
        public string HeadSha => Head.Sha;

        [JsonIgnore]
        public string BaseSha => Base.Sha;

        [JsonIgnore]
        public string HeadBranch => Head.Ref;

        [JsonIgnore]
        public string BaseBranch => Base.Ref;
    }

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

    public record Sender(string Login);
}