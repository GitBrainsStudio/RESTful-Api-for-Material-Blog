using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitBrainsBlogApi.Entities
{
    public class CategoryPostEntity
    {
        public string id { get; set; }
        public string category_id { get; set; }
        public string post_id { get; set; }
    }
}
