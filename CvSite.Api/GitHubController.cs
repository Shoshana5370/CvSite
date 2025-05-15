using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class GitHubController : ControllerBase
{
    private readonly GitHubService.IGitHubClientService _github;

    public GitHubController(GitHubService.IGitHubClientService github)
    {
        _github = github;
    }

    [HttpGet("portfolio")]
    public async Task<IActionResult> GetPortfolio()
    {

        var repos = await _github.GetPortfolioAsync();
        return Ok(repos.Select(r => new {
            r.Name,
            r.Language,
            LastUpdated = r.UpdatedAt,
            r.StargazersCount,
            r.HtmlUrl,
            //r.PullRequestsUrl
        }));
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search(
     [FromQuery] string? name,
     [FromQuery] string? lang,
     [FromQuery] string? user)
    {
        var repos = await _github.SearchRepositoriesAsync(name, lang, user);
        return Ok(repos.Select(r => new {
            r.Name,
            r.Language,
            r.StargazersCount,
            r.HtmlUrl
        }));
    }
}
