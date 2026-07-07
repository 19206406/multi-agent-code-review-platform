namespace WebhookGateway.Common.Models
{
    // sealed 
    public record GithubPrPayload(
        string Action, 
        int Number, 
        PullRequestPayload PullRequest, 
        RepositoryPayload Repository, 
        SenderPayload Sender);

    public record PullRequestPayload(
        int Number, 
        string Title, 
        string State, 
        string Url, 
        string HtmlUrl,
        PullRequestUserPayload User, 
        PullRequestHeadPayload Head,
        PullRequestBasePayload Base, 
        string DiffUrl, 
        DateTime CreatedAt, 
        DateTime UpdatedAt);

    public record PullRequestUserPayload(string Login);
    public record PullRequestHeadPayload(
        string Sha, 
        string Ref);
    public record PullRequestBasePayload(
        string Sha, 
        string Ref);

    // -------------------------------------------------------------- 

    public record RepositoryPayload(
        string FullName, 
        string DefaultBranch, 
        string Language, 
        string CloneUrl, 
        string HtmlUrl);
    public record SenderPayload(string Login); 

}
