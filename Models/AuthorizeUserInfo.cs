using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitBrainsBlogApi.Models
{
    public class AuthorizeUserInfo
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthorizeUserInfo(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }


    }
}
