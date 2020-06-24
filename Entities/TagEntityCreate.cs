using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitBrainsBlogApi.Entities
{
    public class TagEntityCreate : TagEntity
    {
        public TagEntityCreate(string _id, string _title, string _description = "")
        {
            base.id = _id;
            base.title = _title;
            base.description = _description;
        }
    }
}
