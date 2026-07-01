using System.Text.Json.Serialization;

namespace WebhookGateway.Common.Models
{
    public sealed record GithubPrPayload
    {
        [JsonPropertyName("action")]
        public string Action { get; init; } = string.Empty;

        [JsonPropertyName("number")]
        public int Number { get; init; }

        [JsonPropertyName("pull_request")]
        public PullRequest PullRequest { get; init; } = default!;
    }

    public sealed record PullRequest
    {
        [JsonPropertyName("id")]
        public long Id { get; init; }

        [JsonPropertyName("number")]
        public int Number { get; init; }

        [JsonPropertyName("state")]
        public string State { get; init; } = string.Empty;

        [JsonPropertyName("title")]
        public string Title { get; init; } = string.Empty;

        [JsonPropertyName("body")]
        public string? Body { get; init; }

        [JsonPropertyName("html_url")]
        public string HtmlUrl { get; init; } = string.Empty;

        [JsonPropertyName("diff_url")]
        public string DiffUrl { get; init; } = string.Empty;

        [JsonPropertyName("patch_url")]
        public string PatchUrl { get; init; } = string.Empty;

        [JsonPropertyName("commits_url")]
        public string CommitsUrl { get; init; } = string.Empty;

        [JsonPropertyName("user")]
        public GitHubUser User { get; init; } = default!;

        [JsonPropertyName("head")]
        public GitReference Head { get; init; } = default!;
    }

    public sealed record GitReference
    {
        [JsonPropertyName("label")]
        public string Label { get; init; } = string.Empty;

        [JsonPropertyName("ref")]
        public string Ref { get; init; } = string.Empty;

        [JsonPropertyName("sha")]
        public string Sha { get; init; } = string.Empty;

        [JsonPropertyName("user")]
        public GitHubUser User { get; init; } = default!;

        [JsonPropertyName("repo")]
        public GitHubRepository Repo { get; init; } = default!;
    }

    public sealed record GitHubRepository
    {
        [JsonPropertyName("id")]
        public long Id { get; init; }

        [JsonPropertyName("name")]
        public string Name { get; init; } = string.Empty;

        [JsonPropertyName("full_name")]
        public string FullName { get; init; } = string.Empty;

        [JsonPropertyName("private")]
        public bool Private { get; init; }

        [JsonPropertyName("html_url")]
        public string HtmlUrl { get; init; } = string.Empty;

        [JsonPropertyName("clone_url")]
        public string CloneUrl { get; init; } = string.Empty;

        [JsonPropertyName("default_branch")]
        public string? DefaultBranch { get; init; }

        [JsonPropertyName("owner")]
        public GitHubUser Owner { get; init; } = default!;
    }

    public sealed record GitHubUser
    {
        [JsonPropertyName("login")]
        public string Login { get; init; } = string.Empty;

        [JsonPropertyName("id")]
        public long Id { get; init; }

        [JsonPropertyName("html_url")]
        public string HtmlUrl { get; init; } = string.Empty;

        [JsonPropertyName("avatar_url")]
        public string AvatarUrl { get; init; } = string.Empty;

        [JsonPropertyName("type")]
        public string Type { get; init; } = string.Empty;
    }
}
