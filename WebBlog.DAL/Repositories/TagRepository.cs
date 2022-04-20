using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBlog.DAL.EF;
using WebBlog.DAL.Entities;
using WebBlog.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebBlog.DAL.Repositories
{
    public class TagRepository:Repository<Tag>
    {
        public TagRepository(ApplicationContext context) : base(context)
        {        }

        public Tag GetTag(int id)
        {
            return Set.Include(m => m.Posts).Where(m => m.Id == id).FirstOrDefault();
        }

        public IEnumerable<Tag> Find(Func<Tag, Boolean> predicate)
        {
            var rol = Set.Include(m => m.Posts).AsEnumerable().Where(predicate).ToList();
            return rol;
        }

        public List<SelectListItem> GetSelectTags()
        {
            var rl = Set.Include(m => m.Posts).Select(a => new SelectListItem()
                          {
                              Value = a.Id.ToString(),
                              Text = a.Name
                          })
                          .ToList(); 
            return rl;
        }

        public List<SelectListItem> GetSelectTags(Post post)
        {
            var rl = Set.Include(m => m.Posts).Where(m=>m.Posts.Contains(post)).Select(a => new SelectListItem()
            {
                Value = a.Id.ToString(),
                Text = a.Name
            })
                          .ToList();
            return rl;
        }

        public List<SelectListItem> GetSelectTags(List<Tag> ta)
        {
            var rl = Set.ToList();
            List<Tag> j = rl.Except(ta).ToList();
           var t= j.Select(a => new SelectListItem()
            {
                Value = a.Id.ToString(),
                Text = a.Name
            })
                          .ToList();
            return t;
        }

        public IEnumerable<Tag> GetAllTags()
        {
            var rl = Set.Include(m=>m.Posts).ToList();
            return rl;
        }

        /// <summary>
        /// получить список тегов для статьи
        /// </summary>
        /// <param name="post">статья</param>
        /// <returns>список тегов</returns>
        public IEnumerable<Tag> GetbyPost(Post post)
        {
            var rl = Set.Include(m => m.Posts).Where(m => m.Posts.Contains(post)).AsEnumerable();

            return rl;
        }
        public IEnumerable<Tag> GetbyUser(int iduser)
        {
            var rl = Set.Include(m => m.Posts).Where(m => m.UserId==iduser).AsEnumerable();

            return rl;
        }
    }
}
