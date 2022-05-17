using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBlog.DAL.Entities;

namespace WebBlog.Web.Models
{
    public class CommentViewModel
    {
        public IEnumerable<Comment> Comments { get; set; }

    }
}
