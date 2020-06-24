using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitBrainsBlogApi.Entities
{
    public class CategoryEntity
    {
        public string id { get; set; }
        public string seo_name { get; set; }
        public string display_name { get; set; }
        public string logo_64 { get; set; }
    }
}
