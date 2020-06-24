using GitBrainsBlogApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitBrainsBlogApi.Repositories
{
    public class UserRepository : BaseRepository
    {
        public UserEntity FindByUsername(string _userName)
        {
            return QueryFirstOrDefault<UserEntity>("select * from users t where t.username = :username", new { username = _userName });
        }
    }
}
