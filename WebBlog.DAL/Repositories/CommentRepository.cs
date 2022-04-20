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
  public  class CommentRepository : Repository<Comment>
    {
        public CommentRepository(ApplicationContext context) : base(context)
        {
        }

        public Comment Get(int id)
        {
            return Set.Include(m=>m.Post).Include(m=>m.User).FirstOrDefault(m=>m.Id==id);
        }

       public  IEnumerable<Comment> GetAll()
        {
            var rl = Set.Include(m => m.Post).Include(m => m.User).AsEnumerable();
            return rl;
        }



        /// <summary>
        /// получить список комментариев для статьи
        /// </summary>
        /// <param name="post">статья</param>
        /// <returns>список комментариев</returns>
        public IEnumerable<Comment> GetbyPost(Post post)
        {
            var rl = Set.Where(m => m.PostId == post.Id).AsEnumerable();
            return rl;
        }

        /// <summary>
        /// получить список комментариев по атору
        /// </summary>
        /// <param name="post">автор</param>
        /// <returns>список комментариев</returns>
        public IEnumerable<Comment> GetbyUser(User user)
        {
            var rl = Set.Where(m => m.UserId == user.Id).AsEnumerable();
            return rl;
        }
    }
}
