using OrchestratorAgent.Application.Commons.DTOs;
using OrchestratorAgent.Application.Contracts.Services;

namespace OrchestratorAgent.Infrastructure.GitHub;

public class GitHubApiClient : IGitHubApiClient
{
    public Task<List<GitHubFileChangeDto>> GetPullRequestFilesAsync(string owner, string repo, int prNumber, string headSha)
    {
        throw new NotImplementedException();
    }
}
