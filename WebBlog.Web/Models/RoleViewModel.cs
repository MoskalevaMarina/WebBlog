using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBlog.DAL.Entities;

namespace WebBlog.Web.Models
{
    public class RoleViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<User> Users { get; set; }
        // public List<User> Users { get; set; }
         // public RoleViewModel()
       //   {
       //       Users = new List<User>();
      //   }
    }
}
