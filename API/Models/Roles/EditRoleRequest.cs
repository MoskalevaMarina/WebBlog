using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.Roles
{
    public class EditRoleRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
