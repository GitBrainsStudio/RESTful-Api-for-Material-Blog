using GitBrainsBlogApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitBrainsBlogApi.Repositories
{
    public class CategoryPostRepository : BaseRepository
    {
        public int Create(CategoryPostEntityCreate _e)
        {
            return Execute("insert into categories_posts (id,category_id,post_id) values(:id,:category_id,:post_id)", _e);
        }
    }
}
