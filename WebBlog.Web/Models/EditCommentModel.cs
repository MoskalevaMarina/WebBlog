using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBlog.DAL.Entities;

namespace WebBlog.Web.Models
{
    public class EditCommentModel
    {
        public Comment comment { get; set; }
        public IEnumerable<SelectListItem> ListPosts { get; set; }
        public int SelectedPost { set; get; }
       
    }
}
