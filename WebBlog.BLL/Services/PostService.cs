using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBlog.DAL.Entities;
using WebBlog.DAL.Interfaces;
using WebBlog.DAL.Repositories;

namespace WebBlog.BLL.Services
{
    public class PostService
    {
        private IUnitOfWork dbb;
        //   IUnitOfWork dbb;
        //   RoleRepository repository;// = dbb.GetRepository<Role>() as RoleRepository;

        public PostService(IUnitOfWork unitOfWork)
        {
            dbb = unitOfWork;
            //   repository = dbb.GetRepository<Role>() as RoleRepository;

        }

        public IEnumerable<Post> gg()
        {
            var k = dbb.GetRepository<Post>() as PostRepository;
            return k.GetAllPosts();
        }

        public IEnumerable<Post> GetPosts()
        {
            var j = dbb.GetRepository<Post>() as PostRepository;
            return j.GetAllPosts();
        }

        
        public IEnumerable<Post> GetPosrsbyTag(int idtag)
        {
            var k = dbb.GetRepository<Tag>() as TagRepository;
            var p = k.Get(idtag);
            var j = dbb.GetRepository<Post>() as PostRepository;
            return j.Getbytag(p);
        }

        public void AddPosttag(int idtag, int idpost)
        {
            var k = dbb.GetRepository<Tag>() as TagRepository;
            var p = k.Get(idtag);
            var j = dbb.GetRepository<Post>() as PostRepository;
            var j1 = j.GetPostbyid(idpost);
            j.Addtaginpost(p, j1);
        }

        public void AddPostSelecttag(int[] sl, int idpost)
        {
            var k = dbb.GetRepository<Tag>() as TagRepository;
            var j = dbb.GetRepository<Post>() as PostRepository;
            var j1 = j.GetPostbyid(idpost);

           for (int i=0; i<sl.Count(); i++)
            {
                var p = k.Get(sl[i]);
                j.Addtaginpost(p, j1);
            }
        }

        public void DeletePostSelecttag(int[] sl, int idpost)
        {
            var k = dbb.GetRepository<Tag>() as TagRepository;
            var j = dbb.GetRepository<Post>() as PostRepository;
            var j1 = j.GetPostbyid(idpost);

            for (int i = 0; i < sl.Count(); i++)
            {
                var p = k.Get(sl[i]);
                j.Deletetaginpost(p, j1);
            }
        }

        public void AddPost(Post item)
        {
            item.CreateTime = DateTime.Today.ToLongDateString();



            dbb.GetRepository<Post>().Create(item);
        }

        public void UpdatePost(int id1, Post item)
        {
            var r1 = dbb.GetRepository<Post>().Get(id1);
            if (r1 != null)
            {
                r1.Annotation = item.Annotation;
                  r1.Image = item.Image;
                r1.PostText = item.PostText;
                r1.Title = item.Title;
               // r1.Tags = item.Tags;
                //  r1.IsConfirmed = r1.IsConfirmed;
                  r1.UpdateTime = DateTime.Today.ToLongDateString();


            }
            dbb.GetRepository<Post>().Update(r1);
        }



        public void DeletePost(Post item)
        {
            dbb.GetRepository<Post>().Delete(item);
        }


        public Post GetPost(int id)
        {
            var r = dbb.GetRepository<Post>() as PostRepository;
            return r.GetPostbyid(id);
        }

        public IEnumerable<Post> GetPostbyUser(int iduser)
        {
            var us = dbb.GetRepository<User>() as UserRepository;
            var r = dbb.GetRepository<Post>() as PostRepository;
            return r.GetPostsbyuser(iduser);
        }

        public Post GetPostbyTitle(string title)
        {
            var r = dbb.GetRepository<Post>() as PostRepository;
            return r.GetPostbyTitle(title);
        }


        public void Dispose()
        {
            dbb.Dispose();
        }

    }
}
