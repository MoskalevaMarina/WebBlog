using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.Comments
{
    public class AddCommentRequest
    {
        public string TextComment { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
    }
}
