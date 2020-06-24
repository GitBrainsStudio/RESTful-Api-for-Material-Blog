using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitBrainsBlogApi.Entities
{
    public class PostEntityCreate : PostEntity
    {
        public PostEntityCreate(string _guid, string _title, string _preview, string _content, string _authorGuid)
        {
            base.id = _guid;
            base.title = _title;
            base.preview = _preview;
            base.content = _content;
            base.author = _authorGuid;
            base.publish_date = DateTime.Now.ToString();

        }
    }
}
