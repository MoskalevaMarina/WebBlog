using API.Models.Posts;
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
    public class PostsController : ControllerBase
    {
        private CommentService commentService;
        private PostService postService;
        private UserService userService;
        private TagService tagService;
        private IMapper _mapper;

        public PostsController(CommentService service, UserService service1, PostService service2, TagService service3, IMapper mapper)
        {
            commentService = service;
            userService = service1;
            postService = service2;
            tagService = service3;
            _mapper = mapper;
        }


        /// <summary>
        /// Просмотр списка всех статей
        /// </summary>
        [HttpGet]
        // [Route("")]
        public IEnumerable<ViewPost> GetAllPosts()
        {
            return _mapper.Map<IEnumerable<ViewPost>>(postService.GetPosts());
        }

        /// <summary>
        /// Просмотр списка всех статей пользователя по его Id
        /// </summary>
        [HttpGet]
        [Route("{idUser}")]
        public ActionResult<IEnumerable<ViewPost>> GetPostsbyUserId([FromRoute] int idUser)
        {
            var user = userService.GetUser(idUser);
            if (user == null)
                return StatusCode(400, $"Ошибка:Пользователя {idUser} нет.");

            var com = _mapper.Map<IEnumerable<ViewPost>>(postService.GetPostbyUser(user.Id));
            return StatusCode(200, com);
        }

        /// <summary>
        /// Просмотр списка всех статей по  Id тега
        /// </summary>
        [HttpGet]
        [Route("{idTag}")]
        public ActionResult<IEnumerable<ViewPost>> GetPostsbyTagId([FromRoute] int idTag)
        {
            var tag = tagService.GetTag(idTag);
            if (tag == null)
                return StatusCode(400, $"Ошибка:Тега с идентификатором {idTag} нет.");

            var com = _mapper.Map<IEnumerable<ViewPost>>(postService.GetPosrsbyTag(tag.Id));
            return StatusCode(200, com);
        }

        /// <summary>
        /// Просмотр статьи по Id
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        public ActionResult<ViewPost> GetPostById([FromRoute] int id)
        {
            var com = _mapper.Map<ViewPost>(postService.GetPost(id));
            if (com == null)
                return StatusCode(400, $"Ошибка: Статьи с идентификатором {id} не существует.");
            return com;
        }

        /// <summary>
        /// Просмотр статьи по названию
        /// </summary>
        [HttpGet]
        [Route("{title}")]
        public ActionResult<ViewPost> GetPostByTitle([FromRoute] string title)
        {
            var com = _mapper.Map<ViewPost>(postService.GetPostbyTitle(title));
            if (com == null)
                return StatusCode(400, $"Ошибка: Статьи с названием {title} не существует.");
            return com;
        }

        /// <summary>
        /// Добавление статьи
        /// </summary>
        [HttpPost]
        [Route("")]
        public IActionResult AddPost([FromBody] AddPostRequest request)
        {
            var user = userService.GetUser(request.UserId);
            if (user == null)
                return StatusCode(400, $"Ошибка:Пользователя {request.UserId} нет.");

            var newpost = _mapper.Map<AddPostRequest, Post>(request);
            newpost.User = user;
            postService.AddPost(newpost);
            return StatusCode(201, $"Статья  добавлена!");
        }

        /// <summary>
        /// Добавление тега в статью
        /// </summary>
        [HttpPost]
        [Route("")]
        public IActionResult AddTagInPost([FromBody] AddTaginPost request)
        {
            var tag = tagService.GetTag(request.IdTag);
            if (tag == null)
                return StatusCode(400, $"Ошибка: Тег с идентификатором {request.IdTag} не существует.");

            var post = postService.GetPost(request.IdPost);
            if (post == null)
                return StatusCode(400, $"Ошибка: Статья с идентификатором {request.IdPost} не существует.");

            postService.AddPosttag(request.IdTag,request.IdPost);
            return StatusCode(201, $"Тег к статье добавлен !");
        }

        /// <summary>
        /// Обновление статьи
        /// </summary>
        [HttpPut]
        [Route("{id}")]
        public IActionResult EditPostById(
            [FromRoute] int id,
            [FromBody] EditPostRequest request)
        {
            var post = postService.GetPost(id);
            if (post == null)
                return StatusCode(400, $"Ошибка: Статья с идентификатором {id} не существует.");
            else
            {
                var user = userService.GetUser(request.UserId);
                if (user == null)
                    return StatusCode(400, $"Ошибка:Пользователя {request.UserId} нет.");

                var newpost = _mapper.Map<EditPostRequest, Post>(request);
                newpost.User = user;
                postService.UpdatePost(id, newpost);

                return StatusCode(200, $"Статья обновлена! ");
            }
        }

        /// <summary>
        /// Удаление статьи
        /// </summary>
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var com = postService.GetPost(id);
            if (com == null)
                return StatusCode(400, $"Ошибка: Статья с идентификатором {id} не существует.");
            
            postService.DeletePost(com);
            return StatusCode(200, $"Статья удалена!");
        }
    }
}
