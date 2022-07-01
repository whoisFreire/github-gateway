using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using githubApi.Facades.Interfaces;
using githubApi.Models.Jwt;

namespace githubApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IJwtAuthenticationFacade _jwtAuthenticationFacade;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jwtAuthenticationFacade"></param>
        public AuthenticationController(IJwtAuthenticationFacade jwtAuthenticationFacade)
        {
            _jwtAuthenticationFacade = jwtAuthenticationFacade;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="authentication"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> AuthenticateAsync([FromBody] AuthenticationRequest authentication)
        {
            var response = _jwtAuthenticationFacade.Authenticate(authentication.Key);
            return Ok(response);
        }
    }
}
