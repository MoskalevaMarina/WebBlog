using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBlog.DAL.Entities;
using WebBlog.DAL.Interfaces;
using WebBlog.DAL.Repositories;

namespace WebBlog.BLL.Services
{
    public class UserService
    {
        private IUnitOfWork dbb;
        public UserService(IUnitOfWork unitOfWork)
        {
            dbb = unitOfWork;
        }

        public IEnumerable<User> GetUsers()
        {
            var j = dbb.GetRepository<User>() as UserRepository;
            return j.GetAllUsers();
        }

        public IEnumerable<User> GetUsersbyRole(int idrole)
        {
            var k = dbb.GetRepository<Role>() as RoleRepository;
            var p = k.GetRole(idrole);
            var j = dbb.GetRepository<User>() as UserRepository;
            return j.Getbyrole(p);
        }

        public User DelRolebyUser(int userid, int idrole)
        {
            var k = dbb.GetRepository<Role>() as RoleRepository;
            var p = k.GetRole(idrole);
            var j = dbb.GetRepository<User>() as UserRepository;
            return j.DeletebyroleUser(userid, p);
        }

        public User AddRolebyUser(int userid, int? idrole)
        {
            var k = dbb.GetRepository<Role>() as RoleRepository;
            var p = k.GetRole(idrole);
            var j = dbb.GetRepository<User>() as UserRepository;
            return j.AddRoleUser(userid, p);
        }

        public void AddUser(User item)
        {
            item.IsConfirmed = 0;
            item.DataCreate = DateTime.Today.ToLongDateString();
            var k = dbb.GetRepository<Role>() as RoleRepository;
            var j = k.GetbyName("user");
            item.Roles.Add(j);
            dbb.GetRepository<User>().Create(item);
        }

        public void UpdateUser(int id1, User item)
        {
            var r1 = dbb.GetRepository<User>().Get(id1);
            if (r1 != null)
            {
                r1.UserName = item.UserName;
                r1.LastName = item.LastName;
                r1.FirstName = item.FirstName;
                r1.Email = item.Email;
                r1.Avatar = item.Avatar;
                r1.Password = item.Password;
            }
            dbb.GetRepository<User>().Update(r1);
        }

        public void DeleteUser(User item)
        {
            dbb.GetRepository<User>().Delete(item);
        }

        public User GetUser(int id)
        {
            var j = dbb.GetRepository<User>() as UserRepository;
            return j.GetUser(id);
        }

        public User GetUserbyEmail(string email)
        {
            var r = dbb.GetRepository<User>() as UserRepository;
            return r.GetbyEmail(email);
        }

        public User GetUserbyEmailandPassword(string email, string password)
        {
            var r = dbb.GetRepository<User>() as UserRepository;
            return r.Get(email, password);
        }

        public void Dispose()
        {
            dbb.Dispose();
        }
    }
}
