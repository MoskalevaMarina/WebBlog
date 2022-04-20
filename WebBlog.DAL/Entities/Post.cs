using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBlog.DAL.Entities
{
    public class Post
    {
        public int Id { get; set; }
           public int UserId { get; set; } 
        public string Title { get; set; }
        public string PostText { get; set; }
        public string Annotation { get; set; }
        public string CreateTime { get; set; }
        public string UpdateTime { get; set; }
        public string Image { get; set; }

   
        public User User { get; set; }

        public List<Tag> Tags { get; set; }
        public List<Comment> Comments { get; set; }



    }
}
