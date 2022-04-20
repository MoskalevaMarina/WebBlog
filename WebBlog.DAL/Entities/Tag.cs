using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBlog.DAL.Entities
{
   public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int UserId { get; set; }
        public List<Post> Posts { get; set; }
      //  public Tag()
      //  {
      //      Posts = new List<Post>();
      //  }


    }
}
