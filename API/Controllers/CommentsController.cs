using API.Models.Comments;
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
    public class CommentsController : ControllerBase
    {
        private CommentService commentService;
        private PostService postService;
        private UserService userService;
        private IMapper _mapper;

        public CommentsController(CommentService service, UserService service1, PostService service2, IMapper mapper)
        {
            commentService = service;
            userService = service1;
            postService = service2;
            _mapper = mapper;
        }


        /// <summary>
        /// Просмотр списка всех комментариев
        /// </summary>
        [HttpGet]
        // [Route("")]
        public IEnumerable<ViewComment> GetAllComments()
        {
            return _mapper.Map<IEnumerable<ViewComment>>(commentService.GetComments());
        }

        /// <summary>
        /// Просмотр списка всех комментариев пользователя по его Id
        /// </summary>
        [HttpGet]
        [Route("{idUser}")]
        public ActionResult<IEnumerable<ViewComment>> GetCommentsbyUserId([FromRoute] int idUser)
        {
            var user = userService.GetUser(idUser);
            if (user == null)
                return StatusCode(400, $"Ошибка:Пользователя {idUser} нет.");

            var com = _mapper.Map<IEnumerable<ViewComment>>(commentService.GetCommentsbyUser(user.Id));
            return StatusCode(200, com);
        }

        /// <summary>
        /// Просмотр списка всех комментариев по Id статьи
        /// </summary>
        [HttpGet]
        [Route("{idPost}")]
        public ActionResult<IEnumerable<ViewComment>> GetCommentsbyPostId([FromRoute] int idPost)
        {
            var post = postService.GetPost(idPost);
            if (post == null)
                return StatusCode(400, $"Ошибка:Статьи с индетификатором {idPost} нет.");

            var com = _mapper.Map<IEnumerable<ViewComment>>(commentService.GetCommentsbyPost(post.Id));
            return StatusCode(200, com);
        }

        /// <summary>
        /// Просмотр комментария по Id
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        public ActionResult<ViewComment> GetCommentById([FromRoute] int id)
        {
            var com = _mapper.Map<ViewComment>(commentService.GetComment(id));
            if (com == null)
                return StatusCode(400, $"Ошибка: Комментария с идентификатором {id} не существует.");
            return com;
        }

        /// <summary>
        /// Добавление комментария
        /// </summary>
        [HttpPost]
        [Route("")]
        public IActionResult AddComment([FromBody] AddCommentRequest request)
        {
            var user = userService.GetUser(request.UserId);
            if (user == null)
                return StatusCode(400, $"Ошибка:Пользователя {request.UserId} нет.");

            var post = postService.GetPost(request.PostId);
            if (post == null)
                return StatusCode(400, $"Ошибка:Статьи {request.PostId} нет.");

            var newcom = _mapper.Map<AddCommentRequest, Comment>(request);
            newcom.User = user;
            newcom.Post = post;
            commentService.AddComment(newcom);
            return StatusCode(201, $"Комментарий  добавлен!");
        }

        /// <summary>
        /// Обновление существующего комментария
        /// </summary>
        [HttpPut]
        [Route("{id}")]
        public IActionResult EditTagById(
            [FromRoute] int id,
            [FromBody] EditCommentRequest request)
        {
            var com = commentService.GetComment(id);
            if (com == null)
                return StatusCode(400, $"Ошибка: Комментарий с идентификатором {id} не существует.");
            else
            {
                var user = userService.GetUser(request.UserId);
                if (user == null)
                    return StatusCode(400, $"Ошибка:Пользователя {request.UserId} нет.");

                var post = postService.GetPost(request.PostId);
                if (post == null)
                    return StatusCode(400, $"Ошибка:Статьи {request.PostId} нет.");

                var newcom = _mapper.Map<EditCommentRequest, Comment>(request);
                newcom.User = user;
                newcom.Post = post;
                commentService.UpdateComment(id, newcom);
                return StatusCode(200, $"Комментарий обновлен! ");
            }
        }

        /// <summary>
        /// Удаление комментария
        /// </summary>
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var com = commentService.GetComment(id);
            if (com == null)
                return StatusCode(400, $"Ошибка: Комментарий с идентификатором {id} не существует.");
            commentService.DeleteComment(com);
            return StatusCode(200, $"Комментарий удален!");
        }
    }
}
