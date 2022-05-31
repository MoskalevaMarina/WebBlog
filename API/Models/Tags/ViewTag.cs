using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBlog.DAL.Entities;

namespace API.Models.Tags
{
    public class ViewTag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }       
    }
}
