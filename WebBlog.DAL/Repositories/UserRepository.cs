using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBlog.DAL.EF;
using WebBlog.DAL.Entities;

namespace WebBlog.DAL.Repositories
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(ApplicationContext context) : base(context)
        {
        }

        public User GetUser(int? id)
        {
            return Set.Include(m => m.Posts).Include(m => m.Roles).Include(m => m.Tags).Include(m => m.Comments).FirstOrDefault(m => m.Id == id);
        }


        public int GetIdUser(string email)
        {
            return Set.Include(m => m.Comments).Where(m => m.Email == email).FirstOrDefault().Id;
        }

        public IEnumerable<User> Find(Func<User, Boolean> predicate)
        {
            var rol = Set.AsEnumerable().Where(predicate).ToList();
            return rol;
        }

        public IEnumerable<User> GetAllUsers()
        {
            var rl = Set.Include(m => m.Roles).Include(m => m.Posts).Include(m => m.Comments).Include(m => m.Tags).AsEnumerable();
            return rl;
        }

        public IEnumerable<User> Getbyrole(Role role)
        {
            var rl = Set.Include(m => m.Roles).Include(m => m.Posts).Include(m => m.Comments).Where(m => m.Roles.Contains(role)).AsEnumerable();
            return rl;
        }

        public User DeletebyroleUser(int iduser, Role role)
        {
            var rl = Set.Include(m => m.Roles).Include(m => m.Posts).Include(m => m.Tags).Include(m => m.Comments).Where(m => m.Id == iduser).FirstOrDefault();
            rl.Roles.Remove(role);
            return rl;
        }

        public User AddRoleUser(int iduser, Role role)
        {
            var rl = Set.Include(m => m.Roles).Include(m => m.Posts).Include(m => m.Tags).Include(m => m.Comments).Where(m => m.Id == iduser).FirstOrDefault();
            if (!rl.Roles.Contains(role))
            {
                rl.Roles.Add(role);
            }
            return rl;
        }

        public User Get(string name)
        {
            var rl = Set.Include(m => m.Roles).Include(m => m.Posts).Include(m => m.Tags).Include(m => m.Comments).Where(m => m.UserName == name).FirstOrDefault();
            return rl;
        }

        public User Get(string email, string password)
        {
            var rl = Set.Include(m => m.Roles).Include(m => m.Posts).Include(m => m.Tags).Include(m => m.Comments).Where(m => m.Email == email).Where(m => m.Password == password).FirstOrDefault();
            return rl;
        }

        public User GetbyEmail(string name)
        {
            var rl = Set.Include(m => m.Roles).Include(m => m.Posts).Include(m => m.Tags).Include(m => m.Comments).Where(u => u.Email == name).FirstOrDefault();
            return rl;
        }

    }

}
