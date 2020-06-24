using GitBrainsBlogApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitBrainsBlogApi.DTO
{
    public class AccountDTO
    {
        public string userName { get; set; }
        public string token { get; set; }
        public IEnumerable<RolesDTO> roles { get; set; }


        public AccountDTO(UserEntity _e, string _token, IEnumerable<RolesDTO> _roles)
        {
            this.userName = _e.username;
            this.token = _token;
            this.roles = _roles;
        }

        public AccountDTO()
        {

        }
    }
}
