using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBlog.BLL.Services;
using WebBlog.DAL.Entities;
using WebBlog.Web.Models;

namespace WebBlog.Web.Controllers
{
    public class CommentController : Controller
    {
        public CommentService rs;
        private IMapper _mapper;
        private PostService postService;
        private UserService us;

        public CommentController(CommentService rs1, PostService postService1, IMapper mapper, UserService us1)
        {
            rs = rs1;
            _mapper = mapper;
            postService = postService1;
            us = us1;
        }

        [HttpGet]
        public IActionResult Index()
        {

            var sp = rs.GetComments();
            var cm = _mapper.Map<IEnumerable<CommentModel>>(sp);
            return View("Index", cm);
        }

        [HttpGet]
        public IActionResult IndexUserComment()
        {
            if (User.Identity.IsAuthenticated)
            {

                User user = us.GetUserbyEmail(User.Identity.Name);
                if (user != null)
                {
                    var sp = rs.GetCommentsbyUser(user.Id);
                    var cm = _mapper.Map<IEnumerable<CommentModel>>(sp);
                    return View("IndexUser", cm);
                }
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult Create()
        {
            EditCommentModel pm = new EditCommentModel();
            pm.ListPosts = rs.GetPostsSelect();
            return View(pm);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EditCommentModel com1, int SelectedPost)
        {
            if (ModelState.IsValid)
            {
                User user = us.GetUserbyEmail(User.Identity.Name);

                Comment com = com1.comment;
                com.UserId = user.Id;
                com.User = us.GetUser(user.Id);
                com.PostId = SelectedPost;
                com.Post = postService.GetPost(SelectedPost);
                com.DataComment = DateTime.Today.ToLongDateString();

                rs.AddComment(com);
                return RedirectToAction("Index", "Comment");
            }
            else return View(com1);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create1(PostsViewModel pm, int idPost)
        {
            //   if (ModelState.IsValid)
            //   {
            if (pm.NewComment == null)
            {
                return RedirectToAction("Details", "Post", new { id = idPost });
            }
            else
            {
                User user = us.GetUserbyEmail(User.Identity.Name);

                Comment com = new Comment();
                com.TextComment = pm.NewComment;
                com.UserId = user.Id;
                com.User = user;
                com.PostId = idPost;
                com.Post = postService.GetPost(idPost);
                com.DataComment = DateTime.Today.ToLongDateString();

                rs.AddComment(com);

                return RedirectToAction("Details", "Post", new { id = idPost });
            }
            // }
            // else return RedirectToAction("Details", "Post", new { id = idPost });
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id != 0)
            {
                Comment com = rs.GetComment(id);
                if (com != null)
                {
                    var r1 = _mapper.Map<CommentModel>(com);
                    return View(r1);
                }
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, CommentModel com)
        {
            if (ModelState.IsValid)
            {
                var r1 = _mapper.Map<Comment>(com);
                rs.UpdateComment(id, r1);
                if (User.IsInRole("admin"))
                {
                    return RedirectToAction("Index", "Comment");
                }
                else
                {
                    return RedirectToAction("IndexUserComment", "Comment");
                }
            }
            else
                return View(com);

        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id != 0)
            {
                Comment com = rs.GetComment(id);
                if (com != null)
                {
                    rs.DeleteComment(com);
                    if (User.IsInRole("admin"))
                    {
                        return RedirectToAction("Index", "Comment");
                    }
                    else
                    {
                        return RedirectToAction("IndexUserComment", "Comment");
                    }

                }
            }
            return NotFound();
        }
    }
}
