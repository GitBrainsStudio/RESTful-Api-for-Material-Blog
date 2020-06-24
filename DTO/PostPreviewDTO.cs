using GitBrainsBlogApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitBrainsBlogApi.DTO
{
    public class PostPreviewDTO
    {
        public string id { get; set; }
        public string title { get; set; }
        public string preview { get; set; }
        //public IEnumerable<TagDTO> tags { get; set; }
        public CategoryDTO category { get; set; }
        public PostPreviewDTO(PostEntity _entity, CategoryDTO _category)
        {
            this.id = _entity.id;
            this.title = _entity.title;
            this.preview = _entity.preview;
            this.category = _category;
        }
    }
}
