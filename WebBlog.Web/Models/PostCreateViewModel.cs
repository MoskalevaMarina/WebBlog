using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBlog.DAL.Entities;

namespace WebBlog.Web.Models
{
    public class PostCreateViewModel
    {
        public Post post { get; set; }
        public IEnumerable<SelectListItem> ListTags{ get; set; }
        public int[] SelectedTags { set; get; }

        public PostCreateViewModel()
        {
            post = new Post();
        }
        
    }
}
