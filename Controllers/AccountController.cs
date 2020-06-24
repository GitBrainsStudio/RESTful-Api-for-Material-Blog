using GitBrainsBlogApi.Models;
using GitBrainsBlogApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitBrainsBlogApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private Account account;

        public AccountController(Account _account)
        {
            this.account = _account;
        }

        [HttpPost]
        public IActionResult Authenticate([FromBody]JObject _data)
        {
            string _userName = _data["login"].ToObject<string>();
            string _password = _data["password"].ToObject<string>();

            return Ok(new { account = account.Authenticate(_userName, _password), message = "Добро пожаловать на портал, " + _userName } );
        }
    }
}
