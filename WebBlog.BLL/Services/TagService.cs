using Microsoft.AspNetCore.Mvc.Rendering;
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
 public   class TagService
    {
        private IUnitOfWork dbb;

        public TagService(IUnitOfWork unitOfWork)
        {
            dbb = unitOfWork;
        }

        public IEnumerable<Tag> GetTags()
        {
           var k = dbb.GetRepository<Tag>() as TagRepository;
            return k.GetAllTags();
        }

        public IEnumerable<Tag> GetTagAlll()
        {
            var k = dbb.GetRepository<Tag>() as TagRepository;
            return k.GetAllTags();
        }

        public IEnumerable<SelectListItem> GetTagSelectl()
        {
            var k = dbb.GetRepository<Tag>() as TagRepository;
            return k.GetSelectTags();
        }

        public IEnumerable<SelectListItem> GetTagSelectl(Post p)
        {
            var k = dbb.GetRepository<Tag>() as TagRepository;
            return k.GetSelectTags(p);
        }

        public IEnumerable<SelectListItem> GetTagSelectl(List<Tag> tags)
        {
            var k = dbb.GetRepository<Tag>() as TagRepository;
            return k.GetSelectTags(tags);
        }
        public void AddTag(Tag tag)
        {
            dbb.GetRepository<Tag>().Create(tag);
        }

        public void UpdateTag(int id1, Tag tag)
        {
            var r1 = dbb.GetRepository<Tag>().Get(id1);
            if (r1 != null)
            {
                r1.Name = tag.Name;  
            }
            dbb.GetRepository<Tag>().Update(r1);
        }

        public void DeleteTag(Tag tag)
        {
            dbb.GetRepository<Tag>().Delete(tag);
        }
        public Tag GetTag(int id)
        {
            if (id == 0)
                throw new ValidationException("Не установлено id ");

            var y= dbb.GetRepository<Tag>() as TagRepository;
            return y.GetTag(id);
        }

        public Tag GetTag(string name)
        { 
           var y = dbb.GetRepository<Tag>() as TagRepository;
            return y.GetTag(name);
        }

        public IEnumerable<Tag> GetTagbyUser(User user)
        {
            var y = dbb.GetRepository<Tag>() as TagRepository;
            return y.GetbyUser(user.Id);
        }

        public IEnumerable<Tag> GetTagbyPost(Post post)
        {
            var y = dbb.GetRepository<Tag>() as TagRepository;
            return y.GetbyPost(post);
        }

        public void Dispose()
        {
            dbb.Dispose();
        }
    }
}

