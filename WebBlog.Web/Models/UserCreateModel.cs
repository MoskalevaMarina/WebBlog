using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebBlog.DAL.Entities;

namespace WebBlog.Web.Models
{
    public class UserCreateModel
    {
        public int Id { get; set; }

        [Display(Name = "НикНейм")]
        [Required(ErrorMessage = "Введите НикНейм ")]
        public string UserName { get; set; }

        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Введите пароль")]
        [MinLength(5, ErrorMessage = "Минимальная длинна пароля должна быть 5 символов")]
        [MaxLength(15, ErrorMessage = "Максимальная длинна пароля может быть 15 символов")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Введите Email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Имя")]
        public string FirstName { get; set; }
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }
        [Display(Name = "Аватарка")]
        public string Avatar { get; set; }
        [Display(Name = "Подтвержден ли пароль")]
        public int IsConfirmed { get; set; }
        [Display(Name = "Дата регистрации")]
        public string DataCreate { get; set; }
        public List<Tag> Tags { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Post> Posts { get; set; }
        public List<Role> Roles { get; set; }
    }
}
