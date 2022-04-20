using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBlog.DAL.Entities;

namespace WebBlog.Web.Models
{
    public class CommentModel
    {
        public int Id { get; set; }
        public string TextComment { get; set; }
        public string DataComment { get; set; }


        public int? UserId { get; set; }
        public User User { get; set; }
        public int? PostId { get; set; }
        public Post Post { get; set; }
    }
}
