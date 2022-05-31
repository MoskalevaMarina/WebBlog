using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebBlog.DAL.Entities;

namespace WebBlog.Web.Models
{
    public class RoleViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Название роли")]
        [Required(ErrorMessage = "Введите название роли")]
        public string Name { get; set; }

        [Display(Name = "Описание роли")]
        [Required(ErrorMessage = "Введите описание")]
        public string Description { get; set; }
        public List<User> Users { get; set; }      
    }
}
