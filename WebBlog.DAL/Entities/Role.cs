using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBlog.DAL.Entities
{
    public class Role
    {
        public int Id { get; set; }
        [Display (Name ="Наименование")]
        public string Name { get; set; }
        public string Description { get; set; }
        public List<User> Users { get; set; }
      //  public Role()
     //   {
     //       Users = new List<User>();
     //   }
    }
}
