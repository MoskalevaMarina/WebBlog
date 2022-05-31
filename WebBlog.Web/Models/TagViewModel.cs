using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebBlog.DAL.Entities;

namespace WebBlog.Web.Models
{
    public class TagViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Название тега")]
        [Required(ErrorMessage = "Введите название тега")]
        public string Name { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public List<Post> Posts { get; set; }
    }
}
