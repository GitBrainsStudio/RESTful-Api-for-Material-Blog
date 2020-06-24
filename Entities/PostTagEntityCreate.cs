using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitBrainsBlogApi.Entities
{
    public class PostTagEntityCreate
    {
        public string id { get; set; }
        public string post_id { get; set; }
        public string tag_id { get; set; }

        public PostTagEntityCreate(string _id, string _post_id, string _tag_id)
        {
            this.id = _id;
            this.post_id = _post_id;
            this.tag_id = _tag_id;
        }
    }
}
