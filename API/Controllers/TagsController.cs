using API.Models.Tags;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBlog.BLL.Services;
using WebBlog.DAL.Entities;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private TagService tagService;
        private UserService userService;
        private PostService postService;
        private IMapper _mapper;

        public TagsController(TagService service, UserService service1,PostService service2, IMapper mapper)
        {
            tagService = service;
            userService = service1;
            postService = service2;
            _mapper = mapper;
        }


        /// <summary>
        /// Просмотр списка всех тегов
        /// </summary>
        [HttpGet]
        // [Route("")]
        public IEnumerable<ViewTag> GetAllTags()
        {
            var tags = _mapper.Map<IEnumerable<ViewTag>>(tagService.GetTagAlll());
            return tags;
        }

        /// <summary>
        /// Просмотр списка всех тегов пользователя по его Id
        /// </summary>
      [HttpGet]
       [Route("GetTagsbyUserId/{idUser}")]
        public ActionResult<IEnumerable<ViewTag>> GetTagsbyUserId([FromRoute] int idUser)
        {
            var user = userService.GetUser(idUser);
            if (user == null)
                return StatusCode(400, $"Ошибка:Пользователя {idUser} нет.");

            var tags = _mapper.Map<IEnumerable<ViewTag>>(tagService.GetTagbyUser(user));
            return StatusCode(200, tags);
        }

        /// <summary>
        /// Просмотр списка всех тегов статьи по ее  Id
        /// </summary>
        [HttpGet]
        [Route("{idPost}")]
        public ActionResult<IEnumerable<ViewTag>> GetTagsbyPostId([FromRoute] int idPost)
        {
            var post = postService.GetPost(idPost);
            if (post == null)
                return StatusCode(400, $"Ошибка:статьи {idPost} нет.");

            var tags = _mapper.Map<IEnumerable<ViewTag>>(tagService.GetTagbyPost(post));
            return StatusCode(200, tags);
        }

        /// <summary>
        /// Просмотр тега по Id
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        public ActionResult<ViewTag> GetTagById([FromRoute] int id)
        {
            var tag = _mapper.Map<ViewTag>(tagService.GetTag(id));
            if (tag == null)
                return StatusCode(400, $"Ошибка: Тега с идентификатором {id} не существует.");
            return tag;
        }

        /// <summary>
        /// Добавление тега
        /// </summary>
        [HttpPost]
        [Route("")]
        public IActionResult AddTag([FromBody] AddTagRequest request)
        {

            var user = userService.GetUser(request.UserId);
            if (user == null)
                return StatusCode(400, $"Ошибка:Пользователя {request.UserId} нет.");

            var tag = tagService.GetTag(request.Name);
            if (tag == null)
            {
                var newTag = _mapper.Map<AddTagRequest, Tag>(request);
                newTag.User = user;
                tagService.AddTag(newTag);
                return StatusCode(201, $"Тег {request.Name} добавлен!");
            }

            return StatusCode(409, $"Ошибка: Тег {request.Name} уже существует.");
        }

        /// <summary>
        /// Обновление существующего тега
        /// </summary>
        [HttpPut]
        [Route("{id}")]
        public IActionResult EditTagById(
            [FromRoute] int id,
            [FromBody] EditTagRequest request)
        {

            var tag = tagService.GetTag(id);
            if (tag == null)
                return StatusCode(400, $"Ошибка: Тег с идентификатором {id} не существует.");
            else
            {
                var user = userService.GetUser(request.UserId);
                if (user == null)
                    return StatusCode(400, $"Ошибка:Пользователя {request.UserId} нет.");

                var newtag = _mapper.Map<EditTagRequest, Tag>(request);
                tagService.UpdateTag(id, newtag);

                //   rs.UpdateRole(id, new UpdateRoomQuery(request.NewName, request.NewArea, request.NewGasConnected, request.NewVoltage));
                return StatusCode(200, $"Тег обновлен! Имя - {newtag.Name}");

            }

        }
        /// <summary>
        /// Удаление тега
        /// </summary>
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var tag = tagService.GetTag(id);
            if (tag == null)
                return StatusCode(400, $"Ошибка: Тег с идентификатором {id} не существует.");
            tagService.DeleteTag(tag);
            return StatusCode(200, $"Тег удален! Имя - {tag.Name}");
        }

    }
}
