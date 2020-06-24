using GitBrainsBlogApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitBrainsBlogApi.DTO
{
    public class TagDTO
    {
        public string id { get; set; }
        public string title { get; set; }

        public TagDTO(TagEntity _entity)
        {
            this.id = _entity.id;
            this.title = _entity.title;
        }

        public TagDTO()
        {

        }
    }
}
