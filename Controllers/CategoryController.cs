using GitBrainsBlogApi.DTO;
using GitBrainsBlogApi.Models;
using GitBrainsBlogApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitBrainsBlogApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private CategoryRepository _category_repository;
        public CategoryController(CategoryRepository _category_repository)
        {
            this._category_repository = _category_repository;
        }

        [HttpGet]
        public IActionResult AllPreview(string category = "")
        {
            return Ok(_category_repository.FindAll().Select(v => new CategoryDTO(v)));
        }

        [HttpGet("{guid}")]
        public IActionResult Get(string guid)
        {
            return Ok(new CategoryDTO(_category_repository.FindById(guid)));
        }

        [HttpPost]
        public IActionResult Create([FromBody]JObject _data)
        {
            CategoryDTO category = _data.ToObject<CategoryDTO>();

            if (_category_repository.Create(new Entities.CategoryEntityCreate(Guid.NewGuid().ToString(), category.engName, category.rusName, category.logo64Href)) == 0)
                throw new HumanException("Категория не создана. Повторите попытку.");

            return Ok(new { message = "Категория успешно добавлена." });
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(string guid)
        {
            if (_category_repository.Delete(guid) == 0)
                throw new HumanException("Категория не была удалена. Повторите попытку.");
            return Ok(new { message = "Категория успешно удалена." });
        }

        [HttpPut]
        public IActionResult Update([FromBody]JObject _data)
        {
            CategoryDTO category = _data.ToObject<CategoryDTO>();

            if (_category_repository.Update(new Entities.CategoryEntityCreate(category.id, category.engName, category.rusName, category.logo64Href)) == 0)
                throw new HumanException("Категория не была отредактирована. Повторите попытку.");
            return Ok(new { message = "Категория успешно отредактирована." });
        }
    }
}
