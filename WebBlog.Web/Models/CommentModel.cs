using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebBlog.DAL.Entities;

namespace WebBlog.Web.Models
{
    public class CommentModel
    {
        public int Id { get; set; }

        [Display(Name = "Тект коментария")]
        [Required(ErrorMessage = "Вы ничего не написали")]
        public string TextComment { get; set; }
        public string DataComment { get; set; }


        public int? UserId { get; set; }
        public User User { get; set; }
        public int? PostId { get; set; }
        public Post Post { get; set; }
    }
}
