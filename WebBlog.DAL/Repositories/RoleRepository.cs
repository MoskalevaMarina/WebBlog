using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBlog.DAL.EF;
using WebBlog.DAL.Entities;
using WebBlog.DAL.Interfaces;

namespace WebBlog.DAL.Repositories
{
    public class RoleRepository : Repository<Role>
    {
        public RoleRepository(ApplicationContext context) : base(context)
        {

        }

        public Role GetRole(int? id)
        {
            return Set.Include(m => m.Users).FirstOrDefault(m => m.Id == id);
        }

        public IEnumerable<Role> Find(Func<Role, Boolean> predicate)
        {
            var rol = Set.AsEnumerable().Where(predicate).ToList();
            return rol;
        }

        public IEnumerable<Role> GetAllRoles()
        {
            var rl = Set.Include(m => m.Users).AsEnumerable();
            return rl;
        }

        public Role GetbyName(string name)
        {
            var rl = Set.Include(m => m.Users).Where(m => m.Name == name).FirstOrDefault();
            return rl;
        }
    }
}


