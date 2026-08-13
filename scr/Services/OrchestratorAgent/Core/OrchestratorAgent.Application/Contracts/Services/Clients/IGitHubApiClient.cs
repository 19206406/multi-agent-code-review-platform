using OrchestratorAgent.Application.Commons.DTOs;

namespace OrchestratorAgent.Application.Contracts.Services;

public interface IGitHubApiClient
{
    Task<List<GitHubFileChangeDto>> GetPullRequestFilesAsync(string owner, string repo, int prNumber, string headSha);
}