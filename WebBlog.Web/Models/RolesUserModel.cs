using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBlog.DAL.Entities;

namespace WebBlog.Web.Models
{
    public class RolesUserModel
    {
        public User Us { get; set; }     
        public List<Role> Roles {get; set;}
    }
}
