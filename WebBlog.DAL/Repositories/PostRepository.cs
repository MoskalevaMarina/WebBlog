using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBlog.DAL.EF;
using WebBlog.DAL.Entities;

namespace WebBlog.DAL.Repositories
{
    public class PostRepository : Repository<Post>
    {
        public PostRepository(ApplicationContext context): base(context)
        { }

        public Post kk(string ll)
        {
            return Set.Include(m => m.Tags).Include(m => m.Comments).ThenInclude(m => m.User).Include(m => m.User).Where(m => m.Title == ll).FirstOrDefault();

            //  return Set.FirstOrDefault();
        }

        public Post GetPostbyid(int id)
        {
            return Set.Include(m=>m.Tags).Include(m=>m.Comments).ThenInclude(m=>m.User).Include(m => m.User).Where(m=>m.Id==id).FirstOrDefault();
        }

    
        public IEnumerable<Post> Find(Func<Post, Boolean> predicate)
        {
            var rol = Set.AsEnumerable().Where(predicate).ToList();
            return rol;
        }

     /*   public override IEnumerable<Post> GetAll()
        {
            var rl = Set.Include(m => m.Tags).Include(m => m.Comments).AsEnumerable();
            return rl;
        } */

        public IEnumerable<Post> GetAllPosts()
        {
            return Set.Include(m=>m.User).Include(m=>m.Tags).Include(m=>m.Comments).ThenInclude(m => m.User).AsEnumerable();
        }

        public IEnumerable<Post> GetPostsbyuser(int userid)
        {
            return Set.Include(m => m.User).Include(m => m.Tags).Include(m => m.Comments).ThenInclude(m => m.User).Where(m=>m.UserId==userid).AsEnumerable();
        }

        /// <summary>
        /// получить статьи по тегу
        /// </summary>
        /// <param name="tags">тег</param>
        /// <returns>список статей</returns>
        public IEnumerable<Post> Getbytag(Tag tags)
        {
            var rl = Set.Include(m => m.Tags).Include(m => m.User).Include(m => m.Comments).ThenInclude(m => m.User).Where(m => m.Tags.Contains(tags)).AsEnumerable();
            return rl;
        }
        public void Addtaginpost(Tag tag, Post post)
        {
            var rl = Set.Include(m => m.Tags).Include(m => m.User).Include(m => m.Comments).ThenInclude(m => m.User).Where(m => m.Id == post.Id).FirstOrDefault();
            rl.Tags.Add(tag);
            Set.Update(rl);
        }

        public List<SelectListItem> GetSelectPosts()
        {
            var rl = Set.Include(m => m.Tags).Include(m => m.User).Include(m => m.Comments).ThenInclude(m => m.User).Select(a => new SelectListItem()
            {
                Value = a.Id.ToString(),
                Text = a.Title
            })
                          .ToList();
            return rl;
        }
        public void AddCommentinpost(Comment comment, Post post)
        {
            var rl = Set.Include(m => m.Tags).Include(m => m.User).Include(m => m.Comments).ThenInclude(m => m.User).Where(m => m.Id == post.Id).FirstOrDefault();
            rl.Comments.Add(comment);
            Set.Update(rl);
        }

        public void Deletetaginpost(Tag tag, Post post)
        {
            var rl = Set.Include(m => m.Tags).Include(m => m.User).Include(m => m.Comments).ThenInclude(m => m.User).Where(m => m.Id == post.Id).FirstOrDefault();
            rl.Tags.Remove(tag);
            Set.Update(rl);
        }
        /// <summary>
        /// получить статьи по названию
        /// </summary>
        /// <param name="name">название статьи</param>
        /// <returns>список статей</returns>
        public Post GetPostbyTitle(string name)
        {
            var rl = Set.Include(m => m.Tags).Include(m => m.User).Include(m => m.Comments).ThenInclude(m => m.User).Where(m => m.Title == name).FirstOrDefault();
            return rl;
        }

        


    }
}
