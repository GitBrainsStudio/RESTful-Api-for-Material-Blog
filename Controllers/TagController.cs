using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GitBrainsBlogApi.DTO;
using GitBrainsBlogApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GitBrainsBlogApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagController : ControllerBase
    {
        private TagRepository tagRepository;
        public TagController(TagRepository _tagRepository)
        {
            this.tagRepository = _tagRepository;
        }

        [HttpGet]
        public IActionResult All()
        {
            return Ok(tagRepository.FindAll().Select(v => new TagDTO(v)));
        }
        
    }
}