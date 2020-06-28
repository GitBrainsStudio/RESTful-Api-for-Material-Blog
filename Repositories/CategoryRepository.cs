using GitBrainsBlogApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitBrainsBlogApi.Repositories
{
    public class CategoryRepository : BaseRepository
    {
        public IEnumerable<CategoryEntity> FindAll()
        {
            return Query<CategoryEntity>("select * from categories t");
        }

        public CategoryEntity FindById(string _id)
        {
            return QueryFirstOrDefault<CategoryEntity>("select * from categories t where t.id = :id", new { id = _id });
        }

        public CategoryEntity FindByName(string _id)
        {
            return QueryFirstOrDefault<CategoryEntity>("select * from categories t where t.seo_name like :id", new { id = _id });
        }

        public CategoryEntity FindByPostId(string _id)
        {
            return QueryFirstOrDefault<CategoryEntity>(@"select c.* from categories c
                                                        join categories_posts cp on c.id = cp.category_id 
                                                        where cp.post_id = :id", new { id = _id });
        }

        public int Create(CategoryEntityCreate _e)
        {
            return Execute("insert into categories(id,seo_name,display_name,logo_64) values(:id,:seo_name,:display_name,:logo_64)", _e);
        }

        public int Delete(string _id)
        {
            return Execute("delete from categories where id = :id", new { id = _id });
        }

        public int Update(CategoryEntityCreate _e)
        {
            return Execute("update categories set seo_name = :seo_name, display_name = :display_name, logo_64 = :logo_64 where id = :id", _e);
        }
    }
}
