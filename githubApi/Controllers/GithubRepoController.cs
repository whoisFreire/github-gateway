using githubApi.Facades.Interfaces;
using githubApi.Models.Github;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace githubApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class GithubRepoController : ControllerBase
    {
        private readonly IGithubReposFacade _githubRepoFacade;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="githubRepoFacade"></param>
        public GithubRepoController(IGithubReposFacade githubRepoFacade)
        {
            _githubRepoFacade = githubRepoFacade;
        }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="username"></param>
    /// <param name="language"></param>
    /// <returns></returns>
        [HttpGet("{username}")]
        [Authorize]
        public async Task<IActionResult> GetReposAsync([FromRoute] string username, [FromQuery] string language)
        {
            List<GithubRepo> response;
            if (language != null)
            {
                response = await _githubRepoFacade.GetGithubReposByLanguageAsync(username, language);
                return Ok(response);
            }
            response = await _githubRepoFacade.GetGithubReposAsync(username);

            return Ok(response);
        }
    }
}
