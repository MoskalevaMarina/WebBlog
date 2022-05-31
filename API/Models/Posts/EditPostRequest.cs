using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.Posts
{
    public class EditPostRequest
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public string PostText { get; set; }
        public string Annotation { get; set; }
        public string Image { get; set; }
    }
}
