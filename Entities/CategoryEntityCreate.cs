using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitBrainsBlogApi.Entities
{
    public class CategoryEntityCreate : CategoryEntity
    {
        public CategoryEntityCreate(string _id, string _seo_name, string _display_name, string _href_logo)
        {
            this.id = _id;
            this.seo_name = _seo_name;
            this.display_name = _display_name;
            this.logo_64 = _href_logo;
        }
    }
}
