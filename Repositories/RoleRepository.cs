using GitBrainsBlogApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitBrainsBlogApi.Repositories
{
    public class RoleRepository : BaseRepository
    {
        public IEnumerable<RolesDTO> FindByUserId(string _userGuid)
        {
            return Query<RolesDTO>(@"select r.title from users_roles ur 
                                        join users u on ur.user_id = u.id
                                        join roles r on r.id = ur.role_id
                                        where u.id = :userGuid", new { userGuid = _userGuid });
        }
    }
}
