using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubService
{
    public interface IGitHubClientService
    {
        public  Task<IReadOnlyList<Repository>> SearchRepositoriesAsync(string? name, string? language, string? user);
        public  Task<IReadOnlyList<Repository>> GetPortfolioAsync();


    }
}
