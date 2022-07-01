using githubApi.Models.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace githubApi.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken();
    }
}
