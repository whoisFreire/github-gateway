using githubApi.Models.Github;
using RestEase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace githubApi.Services.RestEase
{
    [Header("User-Agent", "Github Client RestEase")]
    public interface IGithubClient
    {
        [Get("users/{username}/repos")]
        Task<List<GithubRepo>> GetGithubReposAsync([Path("username")] string username,
                                                   CancellationToken cancellationToken);
    }
}
