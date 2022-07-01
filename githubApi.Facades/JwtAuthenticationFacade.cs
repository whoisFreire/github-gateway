using githubApi.Facades.Interfaces;
using githubApi.Models.UI;
using githubApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace githubApi.Facades
{
    public class JwtAuthenticationFacade : IJwtAuthenticationFacade
    {
        private readonly IJwtService _jwtService;
        private readonly ApiSettings _apiSettings;
        public JwtAuthenticationFacade(IJwtService jwtService, ApiSettings apiSettings)
        {
            _jwtService = jwtService;
            _apiSettings = apiSettings;
        }

        public string Authenticate(string keyAuthentication)
        {
            var isAuthorized = keyAuthentication.Equals(_apiSettings.JwtSettings.AuthenticationKey);

            if (!isAuthorized) return "forbidden";

            var token = _jwtService.GenerateToken();

            return token;
        }
    }
}
