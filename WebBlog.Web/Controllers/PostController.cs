using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebBlog.BLL.Services;
using WebBlog.DAL.Entities;
using WebBlog.Web.Models;

namespace WebBlog.Web.Controllers
{
    public class PostController : Controller
    {
        public PostService rs;
        private IMapper _mapper;
        private UserService us;
        private TagService ts;
        IWebHostEnvironment _appEnvironment;

        public PostController(PostService rs1, UserService us1, TagService ts1, IMapper mapper, IWebHostEnvironment w)
        {
            rs = rs1;
            _mapper = mapper;
            us = us1;
            ts = ts1;
            _appEnvironment = w;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var sp = _mapper.Map<IEnumerable<PostsViewModel>>(rs.GetPosts());
            return View(sp);
        }

        [HttpGet]
        public IActionResult IndexUserPost()
        {
            if (User.Identity.IsAuthenticated)
            {

                User user = us.GetUserbyEmail(User.Identity.Name);
                if (user != null)
                {


                    var sp = _mapper.Map<IEnumerable<PostsViewModel>>(rs.GetPostbyUser(user.Id));
                    return View(sp);


                }

            }
            return NotFound();

        }

        [HttpGet]
        public IActionResult Create()
        {
            PostsViewModel pm = new PostsViewModel();
            pm.ListTags = ts.GetTagSelectl();

            return View(pm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PostsViewModel post, IFormFile upload)
        {
            if (ModelState.IsValid)
            {
                string path;
                if (upload != null)
                {
                    // путь к папке Files
                    path = "/images/images_post/" + upload.FileName;
                    // сохраняем файл в папку Files в каталоге wwwroot
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        upload.CopyTo(fileStream);
                    }
                }
                else
                {
                    path = "/images/images_post/article2.jpg";
                }

                post.Image = path;
                var pp = us.GetUserbyEmail(User.Identity.Name);
                post.UserId = pp.Id;

                post.CreateTime = DateTime.Today.ToLongDateString();
                var r1 = _mapper.Map<Post>(post);

                //  r1.UserId = iduser;
                r1.User = us.GetUser(r1.UserId);

                rs.AddPost(r1);

                int gg = rs.GetPosts().Where(m => m.Title == r1.Title).LastOrDefault().Id;


                if (post.SelectedTags != null)
                {
                    rs.AddPostSelecttag(post.SelectedTags, gg);
                }
                rs.UpdatePost(gg, r1);

                return RedirectToAction("IndexUserPost", "Post");

            }
            else return View(post);
        }


        public IActionResult Details(int id)
        {
            if (id != 0)
            {
                Post sp = rs.GetPost(id);

                if (sp != null)
                {
                    var sp1 = _mapper.Map<PostsViewModel>(sp);
                    return View(sp1);
                }
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id != 0)
            {
                Post post = rs.GetPost(id);
                if (post != null)
                {
                    var pm = _mapper.Map<PostEditViewModel>(post);
                    //  PostEditViewModel pm = new PostEditViewModel();
                    //    pm.post = post;
                    pm.ListTagsAdd = ts.GetTagSelectl(pm.Tags);
                    pm.ListTagsDel = ts.GetTagSelectl(post);
                    pm.SelectedTagsAdd = new int[pm.ListTagsAdd.Count()];
                    pm.SelectedTagsDel = new int[pm.ListTagsDel.Count()];
                    return View(pm);
                }
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PostEditViewModel com, IFormFile upload)
        {
            if (ModelState.IsValid)
            {
                if (com.Id != 0)
                {
                    Post post = rs.GetPost(com.Id);


                    if (post != null)
                    {
                        string path;
                        if (upload != null)
                        {
                            // путь к папке Files
                            path = "/images/images_post/" + upload.FileName;
                            // сохраняем файл в папку Files в каталоге wwwroot
                            using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                            {
                                upload.CopyTo(fileStream);
                            }
                        }
                        else
                        {
                            path = "/images/images_post/article2.jpg";
                        }


                        var r1 = _mapper.Map<Post>(com);
                        r1.Image = path;

                        if (com.SelectedTagsAdd != null)
                        {
                            rs.AddPostSelecttag(com.SelectedTagsAdd, post.Id);
                        }

                        if (com.SelectedTagsDel != null)
                        {
                            rs.DeletePostSelecttag(com.SelectedTagsDel, post.Id);
                        }

                        rs.UpdatePost(com.Id, r1);
                    }
                }

                return RedirectToAction("IndexUserPost", "Post");
            }
            else return View(com);

        }



        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id != 0)
            {
                Post post = rs.GetPost(id);
                if (post != null)
                {
                    rs.DeletePost(post);
                    return RedirectToAction("IndexUserPost", "Post");
                }
            }
            return NotFound();
        }
    }
}
