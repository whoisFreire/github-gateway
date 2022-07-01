using githubApi.Facades.Interfaces;
using githubApi.Models.Github;
using githubApi.Services.RestEase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace githubApi.Facades
{
    public class GithubReposFacade : IGithubReposFacade
    {
        private readonly IGithubClient _githubClientRestEase;

        public GithubReposFacade(IGithubClient githubClientRestEase)
        {
            _githubClientRestEase = githubClientRestEase;
        }

        public async Task<List<GithubRepo>> GetGithubReposAsync(string username)
        {
            try
            {
                var repos = await _githubClientRestEase.GetGithubReposAsync(username, CancellationToken.None);

                return repos;
            }catch(Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        public async Task<List<GithubRepo>> GetGithubReposByLanguageAsync(string username, string language)
        {
            try
            {
                var repos = await _githubClientRestEase.GetGithubReposAsync(username,
                                                                            CancellationToken.None);

                var searchQuery = repos.Where(repo => !string.IsNullOrEmpty(repo.Language) 
                                              && repo.Language.ToLower() == language);

                var reposByLanguage = searchQuery.ToList();
                return reposByLanguage;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }
    }
}
