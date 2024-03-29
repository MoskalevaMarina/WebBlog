﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBlog.DAL.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Display(Name = "НикНейм")]
        public string UserName { get; set; }

        [Display(Name = "Пароль")]
        public string Password { get; set; }
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

        public User()
        {
            Tags = new List<Tag>();
            Comments = new List<Comment>();
            Posts = new List<Post>();
            Roles = new List<Role>();
        }
    }
}
