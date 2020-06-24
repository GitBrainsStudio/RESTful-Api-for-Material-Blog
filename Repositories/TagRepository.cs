using GitBrainsBlogApi.DTO;
using GitBrainsBlogApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitBrainsBlogApi.Repositories
{
    public class TagRepository: BaseRepository
    {
        public IEnumerable<TagEntity> FindAll()
        {
            return Query<TagEntity>("select * from tags");
        }

        public TagEntity Find(string _name)
        {
            return QueryFirstOrDefault<TagEntity>("select * from tags where title like :name", new { name = _name });
        }

        public IEnumerable<TagEntity> FindByPost(string _post_guid)
        {
            return Query<TagEntity>(@"select t.id, t.title from tags t 
                                    join posts_tags pt on t.id = pt.tag_id 
                                    and pt.post_id = :post_guid", new { post_guid = _post_guid });
        }

        public int Create(TagEntityCreate _entity)
        {
            return Execute("insert into tags(id,title,description) values(:id,:title,:description)", _entity);
        }
    }
}
