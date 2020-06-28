using GitBrainsBlogApi.Models;
using GitBrainsBlogApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using GitBrainsBlogApi.Entities;
using GitBrainsBlogApi.DTO;

namespace GitBrainsBlogApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private CategoryRepository _category_repository;
        private CategoryPostRepository _category_post_repository;
        private PostTagRepository _post_tag_repo;
        private TagRepository _tag_repo;
        private PostRepository _post_repo;
        private Account _account;

        public PostController(Account _account, PostRepository _post_repo, TagRepository _tag_repo, PostTagRepository _post_tag_repo, CategoryPostRepository _category_post_repository, CategoryRepository _category_repository)
        {
            this._account = _account;
            this._post_repo = _post_repo;
            this._tag_repo = _tag_repo;
            this._post_tag_repo = _post_tag_repo;
            this._category_repository = _category_repository;
            this._category_post_repository = _category_post_repository;
        }

        
        [Route("preview/{sort}")]
        public IActionResult AllPreview(string sort)
        {
            if (sort == "popular") return Ok(_post_repo.FindAll().OrderByDescending(v => Convert.ToInt32(v.views)).ToList().Select(v => new PostPreviewDTO(v, new CategoryDTO(_category_repository.FindByPostId(v.id)))));
            if (sort == "new") return Ok(_post_repo.FindAll().OrderByDescending(v => Convert.ToDateTime(v.publish_date)).ToList().Select(v => new PostPreviewDTO(v, new CategoryDTO(_category_repository.FindByPostId(v.id)))));
            throw new HumanException("Тип сортировки не определен.");
        }

        [Route("preview/{category}/{sort}")]
        public IActionResult AllPreview(string category, string sort)
        {
            var findCategory = _category_repository.FindByName(category);
            if (findCategory is null) return NotFound("Категория не найдена");

            if (sort == "popular") return Ok(_post_repo.FindByCategoryId(findCategory.id).OrderByDescending(v => Convert.ToInt32(v.views)).ToList().Select(v => new PostPreviewDTO(v, new CategoryDTO(_category_repository.FindByPostId(v.id)))));
            if (sort == "new") return Ok(_post_repo.FindByCategoryId(findCategory.id).OrderByDescending(v => Convert.ToDateTime(v.publish_date)).ToList().Select(v => new PostPreviewDTO(v, new CategoryDTO(_category_repository.FindByPostId(v.id)))));
            throw new HumanException("Тип сортировки не определен.");
        }



        [HttpGet("{guid}")]
        public IActionResult Details(string guid)
        {
            var findPost = _post_repo.Find(guid);
            if (findPost is null) throw new HumanException("Статья не найдена. Проверьте корректность введённой ссылки.");

            _post_repo.UpdateViewsByPost(findPost.id, findPost.views + 1);

            return Ok(new PostDTO(findPost, _tag_repo.FindByPost(findPost.id).Select(t => new TagDTO(t))));
        }

        [Authorize]
        [HttpPost]
        public IActionResult Publish([FromBody]JObject _data)
        {
            //мапинг входящих параметров их json объекта
            string _title = _data["title"].ToObject<string>();
            string _preview = _data["preview"].ToObject<string>();
            string _content = _data["content"].ToObject<string>();
            string authorGuid = _account.Claims().FirstOrDefault(x => x.Type == "g").Value;

            //генерация нового гуида для статьи
            string _post_guid = Guid.NewGuid().ToString();
            CategoryDTO _category = _data["category"].ToObject<CategoryDTO>();

            //проверка на существование категории
            if (_category_repository.FindById(_category.id) is null) throw new HumanException("Категория не существует.");

            //проверка на запись в бд (1 - успешно)
            if (_post_repo.Create(new PostEntityCreate(_post_guid, _title, _preview, _content, authorGuid)) == 0) 
                throw new HumanException("К сожалению, статья не была добавлена. Повторите попытку ещё раз.");

            //запись в таблицу связей (категория - пост)
            if (_category_post_repository.Create(new CategoryPostEntityCreate(Guid.NewGuid().ToString(), _category.id, _post_guid)) == 0)
                throw new HumanException("К сожалению, категороия не была добавлена. Повторите попытку ещё раз.");


            return Ok(new { message = "Пост успешно опубликован." });

            //валидация тегов
            //List<TagDTO> tags = _data["tags"].ToObject<List<TagDTO>>();
            //if (tags.Count == 0) throw new HumanException("Необходимо указать как минимум один тэг для статьи.");


            //добавление тегов к посту
            //tags.ForEach(v =>
            //{
            //    if (String.IsNullOrEmpty(v.id))
            //    {

            //        string _tag_guid = Guid.NewGuid().ToString();
            //        _tag_repo.Create(new TagEntityCreate(_tag_guid, v.title, ""));
            //        v.id = _tag_guid;
            //    }
            //    string _post_tag_guid = Guid.NewGuid().ToString();
            //    _post_tag_repo.Create(new PostTagEntityCreate(_post_tag_guid, _post_guid, v.id));
            //});
        }

        [Authorize]
        [HttpPut]
        public IActionResult Update([FromBody]JObject _data)
        {
            string _title = _data["title"].ToObject<string>();
            string _preview = _data["preview"].ToObject<string>();
            string _content = _data["content"].ToObject<string>();
            string authorGuid = _account.Claims().FirstOrDefault(x => x.Type == "g").Value;
            string _post_guid = _data["id"].ToObject<string>();

            //List<TagDTO> tags = _data["tags"].ToObject<List<TagDTO>>();
            //if (tags.Count == 0) throw new HumanException("Необходимо указать как минимум один тэг для статьи.");

            //if (_post_tag_repo.DeleteTagsByPost(_post_guid) == 0)
            //    throw new HumanException("К сожалению, тэги не были удалены.");

            if (_post_repo.Update(new PostEntityCreate(_post_guid, _title, _preview, _content, authorGuid)) == 0)
                throw new HumanException("К сожалению, пост не был отредактирован.");


            //tags.ForEach(v =>
            //{
            //    if (String.IsNullOrEmpty(v.id)) {

            //        string _tag_guid = Guid.NewGuid().ToString();
            //        _tag_repo.Create(new TagEntityCreate(_tag_guid, v.title, ""));
            //        v.id = _tag_guid;
            //    }
            //    string _post_tag_guid = Guid.NewGuid().ToString();
            //    _post_tag_repo.Create(new PostTagEntityCreate(_post_tag_guid, _post_guid, v.id));
            //});

            return Ok(new { message = "Пост успешно отредактирован." });

        }

        [Authorize]
        [HttpDelete("{guid}")]
        public IActionResult Delete(string guid)
        {
            if (_post_repo.Delete(guid) == 0) throw new HumanException("К сожалению, пост не был удалён. Повторите попытку ещё раз.");

            return Ok(new { message = "Пост успешно удалён." });
        }
    }
    
}

