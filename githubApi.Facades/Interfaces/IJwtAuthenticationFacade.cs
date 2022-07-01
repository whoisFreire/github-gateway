using System;
using System.Collections.Generic;
using System.Text;

namespace githubApi.Facades.Interfaces
{
    public interface IJwtAuthenticationFacade
    {
        string Authenticate(string keyAuthentication);
    }
}
