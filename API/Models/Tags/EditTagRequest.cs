using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.Tags
{
    public class EditTagRequest
    {
        public string Name { get; set; }
        public int UserId { get; set; }
    }
}
