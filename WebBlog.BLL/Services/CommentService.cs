﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBlog.DAL.Entities;
using WebBlog.DAL.Interfaces;
using WebBlog.DAL.Repositories;

namespace WebBlog.BLL.Services
{
    public class CommentService
    {
        private IUnitOfWork dbb;

        public CommentService(IUnitOfWork unitOfWork)
        {
            dbb = unitOfWork;
        }

        public IEnumerable<Comment> GetComments()
        {
            var k = dbb.GetRepository<Comment>() as CommentRepository;
            return k.GetAllComments();
        }

        public IEnumerable<Comment> GetCommentsbyUser(int iduser)
        {
            var l = dbb.GetRepository<User>() as UserRepository;
            var us1 = l.GetUser(iduser);
            var k = dbb.GetRepository<Comment>() as CommentRepository;
            return k.GetbyUser(us1);
        }

        public IEnumerable<Comment> GetCommentsbyPost(int idpost)
        {
            var l = dbb.GetRepository<Post>() as PostRepository;
            var post1 = l.Get(idpost);
            var k = dbb.GetRepository<Comment>() as CommentRepository;
            return k.GetbyPost(post1);
        }


        public void AddComment(Comment comment)
        {
            comment.DataComment = DateTime.Today.ToLongDateString();
            dbb.GetRepository<Comment>().Create(comment);
        }

        public void UpdateComment(int id1, Comment comment)
        {
            var r2 = dbb.GetRepository<Comment>() as CommentRepository;
            var r1 = r2.GetComment(id1);
            if (r1 != null)
            {
                r1.TextComment = comment.TextComment;
            }
            dbb.GetRepository<Comment>().Update(r1);
        }

        public void DeleteComment(Comment comment)
        {
            dbb.GetRepository<Comment>().Delete(comment);
        }
        public Comment GetComment(int id)
        {
            var r2 = dbb.GetRepository<Comment>() as CommentRepository;
            var r1 = r2.GetComment(id);
            return r1;
        }

        public IEnumerable<SelectListItem> GetPostsSelect()
        {
            var k = dbb.GetRepository<Post>() as PostRepository;
            return k.GetSelectPosts();
        }

        public void Dispose()
        {
            dbb.Dispose();
        }
    }
}
