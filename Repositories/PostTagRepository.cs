using GitBrainsBlogApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitBrainsBlogApi.Repositories
{
    public class PostTagRepository : BaseRepository
    {
        public int Create(PostTagEntityCreate _entity)
        {
            return Execute("insert into posts_tags(id,post_id,tag_id) values(:id,:post_id,:tag_id)", _entity);
        }

        public int DeleteTagsByPost(string _id)
        {
            return Execute("delete from posts_tags where post_id = :id", new { id = _id });
        }
    }
}
