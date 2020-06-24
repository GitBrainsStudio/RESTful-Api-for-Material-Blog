using GitBrainsBlogApi.Entities;
using GitBrainsBlogApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitBrainsBlogApi.DTO
{
    public class CategoryDTO
    {
        public string id { get; set; }
        public string engName { get; set; }
        public string rusName { get; set; }
        public string logo64Href { get; set; }

        public CategoryDTO(CategoryEntity _entity)
        {
            if (_entity != null)
            {
                this.id = _entity.id;
                this.engName = _entity.seo_name;
                this.rusName = _entity.display_name;
                this.logo64Href = _entity.logo_64;
            }
        }

        public CategoryDTO()
        {

        }
    }
}
