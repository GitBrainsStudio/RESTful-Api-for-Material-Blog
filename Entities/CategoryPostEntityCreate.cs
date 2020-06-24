using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitBrainsBlogApi.Entities
{
    public class CategoryPostEntityCreate : CategoryPostEntity
    {
        public CategoryPostEntityCreate(string _id, string _category_id, string _post_id)
        {
            base.id = _id;
            base.category_id = _category_id;
            base.post_id = _post_id;
        }
    }
}
