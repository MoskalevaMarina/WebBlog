using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.Posts
{
    public class ViewPost
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string PostText { get; set; }
        public string Annotation { get; set; }
        public string CreateTime { get; set; }
        public string UpdateTime { get; set; }
        public string Image { get; set; }
    }
}
