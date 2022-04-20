using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBlog.DAL.Entities;

namespace WebBlog.Web.Models
{
    public class PostEditViewModel
    {
        public Post post { get; set; }
        public IEnumerable<SelectListItem> ListTagsAdd { get; set; }
        public int[] SelectedTagsAdd { set; get; }

        public IEnumerable<SelectListItem> ListTagsDel { get; set; }
        public int[] SelectedTagsDel { set; get; }


    }
}
