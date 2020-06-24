using GitBrainsBlogApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitBrainsBlogApi.DTO
{
    public class PostDTO
    {
        public string id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string preview { get; set; }
        public IEnumerable<TagDTO> tags { get; set; }

        public PostDTO(PostEntity _entity, IEnumerable<TagDTO> _tags)
        {
            this.id = _entity.id;
            this.title = _entity.title;
            this.content = _entity.content;
            this.preview = _entity.preview;
            this.tags = _tags;
        }
    }
}
