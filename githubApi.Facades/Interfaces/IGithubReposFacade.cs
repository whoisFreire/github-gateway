using githubApi.Models.Github;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace githubApi.Facades.Interfaces
{
    public interface IGithubReposFacade
    {
        Task<List<GithubRepo>> GetGithubReposAsync(string username);

        Task<List<GithubRepo>> GetGithubReposByLanguageAsync(string username, string language);

    }
}
