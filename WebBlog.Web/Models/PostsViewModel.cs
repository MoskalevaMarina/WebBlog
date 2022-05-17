using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebBlog.DAL.Entities;

namespace WebBlog.Web.Models
{
    public class PostsViewModel
    {
        public int Id { get; set; }

        [Display(Name ="Название статьи")]
        [Required(ErrorMessage ="Введите название статьи")]
        public string Title { get; set; }

        [Display(Name = "Текст статьи")]
        [Required(ErrorMessage = "Введите текст статьи")]
        public string PostText { get; set; }

        [Display(Name = "Аннотация статьи")]
        [Required(ErrorMessage = "Введите аннотацию статьи")]
        public string Annotation { get; set; }
        public string CreateTime { get; set; }
        public string UpdateTime { get; set; }

        [Display(Name = "Картинка для статьи")]
         public string Image { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public List<Tag> Tags { get; set; }
        public List<Comment> Comments { get; set; }

        public IEnumerable<SelectListItem> ListTags { get; set; }
        public int[] SelectedTags { set; get; }
        public string NewComment { get; set; }

    }
}
