using Octokit;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Caching.Memory;
using GitHubService;

public class GitHubClientService:IGitHubClientService
{
    private readonly GitHubClient _client;
    private readonly GitHubSettings _settings;
    private readonly IMemoryCache _cache;

    public GitHubClientService(IOptions<GitHubSettings> options, IMemoryCache cache)
    {
        _settings = options.Value;
        _cache = cache;
        _client = new GitHubClient(new ProductHeaderValue("CvSiteApp"))
        {
            Credentials = new Credentials(_settings.Token)
        };
    }

    public async Task<IReadOnlyList<Repository>> GetPortfolioAsync()
    {
        string cacheKey = $"portfolio-{_settings.Username}";
        if (_cache.TryGetValue(cacheKey, out IReadOnlyList<Repository> cachedRepos))
        {
            return cachedRepos;
        }
        var repos = await _client.Repository.GetAllForUser(_settings.Username);
        _cache.Set(cacheKey, repos, TimeSpan.FromMinutes(10));

        return repos;
    }

    public async Task<IReadOnlyList<Repository>> SearchRepositoriesAsync(string? name, string? language, string? user)
    {
        var query = string.IsNullOrWhiteSpace(name) ? "repo" : name;
        var request = new SearchRepositoriesRequest(query)
        {
            User = user,
            SortField = RepoSearchSort.Updated,
            Order = SortDirection.Descending
        };

        if (!string.IsNullOrWhiteSpace(language))
        {
            try
            {
                language = language switch
                {
                    "C#" => "CSharp",
                    "C++" => "Cpp",
                    _ => language
                };

                request.Language = string.IsNullOrWhiteSpace(language) ? null : (Language?)Enum.Parse(typeof(Language), language, true);


            }
            catch
            {
            }
        }

        if (!string.IsNullOrWhiteSpace(user))
        {
            request.User = user;
        }

        var result = await _client.Search.SearchRepo(request);
        return result.Items;
    }
}

