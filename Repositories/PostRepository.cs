using GitBrainsBlogApi.Entities;
using System.Collections.Generic;

namespace GitBrainsBlogApi.Repositories
{
    public class PostRepository : BaseRepository
    {
        public IEnumerable<PostEntity> FindAll()
        {
            string query = @"select * from posts";
            return Query<PostEntity>(query);
        }

        public IEnumerable<PostEntity> FindAllOrderBy(string _sort)
        {
            string query = @"select * from posts";
            if (_sort == "popular") query += " order by views";
            if (_sort == "new") query += " order by date(publish_date)";
            return Query<PostEntity>(query);
        }

        public IEnumerable<PostEntity> FindByTagName(string _tagName)
        {
            return Query<PostEntity>(@"select pt.* from posts pt
                                        left join posts_tags p on pt.id = p.post_id
                                        left join tags t on p.tag_id = t.id 
                                        where t.title like :tag", new { tag = _tagName });
        }

        public IEnumerable<PostEntity> FindByTagId(string _id)
        {
            return Query<PostEntity>(@"select p.* from posts_tags pt 
                                        join posts p on pt.post_id = p.id and pt.tag_id = :id", new { id = _id });
        }

        public IEnumerable<PostEntity> FindByCategoryId(string _id)
        {

            string query = @"select c.* from posts c
                                       join categories_posts cp on c.id = cp.post_id 
                                       where cp.category_id = :id";


            return Query<PostEntity>(query, new { id = _id });
        }

        public PostEntity Find(string _guid)
        {
            return QueryFirstOrDefault<PostEntity>("select * from posts t where t.id = :guid", new { guid = _guid });
        }

 
        public int Create(PostEntityCreate entity)
        {
            return Execute("insert into posts(id,title,preview,content,author,publish_date) values(:id,:title,:preview,:content,:author,:publish_date)", entity);
        }

        public int Update(PostEntityCreate entity)
        {
            return Execute("update posts set title = :title, preview = :preview, content = :content where id = :id", entity);
        }

        public int Delete(string _guid)
        {
            return Execute("delete from posts where id = :guid", new { guid = _guid });
        }

        public int UpdateViewsByPost(string _guid, int _views)
        {
            return Execute("update posts set views = :views where id = :guid", new { guid = _guid, views = _views });
        }
    }
}
