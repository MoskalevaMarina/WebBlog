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
   public class RoleService
    {
        private IUnitOfWork dbb;
        //   IUnitOfWork dbb;
        //   RoleRepository repository;// = dbb.GetRepository<Role>() as RoleRepository;

        public RoleService(IUnitOfWork unitOfWork)
        {
            dbb = unitOfWork;
         //   repository = dbb.GetRepository<Role>() as RoleRepository;

        }
       

        public IEnumerable<Role> GetRoles()
        {
           var k=  dbb.GetRepository<Role>() as RoleRepository;
            return k.GetAllRoles();
        }

        public void AddRole(Role role)
        {
            dbb.GetRepository<Role>().Create(role);
        }

        public void UpdateRole(int id1,Role role)
        {
             var r1 = dbb.GetRepository<Role>().Get(id1);
            if(r1!=null)
            {
                r1.Name = role.Name;
                r1.Description = role.Description;
              //  r1.Users = role.Users;

            }
           dbb.GetRepository<Role>().Update(r1);
        }

       

        public void DeleteRole(Role role)
        {
            dbb.GetRepository<Role>().Delete(role);
        }
        public Role GetRole(int id)
        {
            var k = dbb.GetRepository<Role>() as RoleRepository;
            return k.GetRole(id);
        }

        public Role GetRole(string namerole)
        {
            var k = dbb.GetRepository<Role>() as RoleRepository;
            return k.GetbyName(namerole);
        }
        /* public PhoneDTO GetPhone(int? id)
         {
             if (id == null)
                 throw new ValidationException("Не установлено id телефона", "");
             var phone = Database.Phones.Get(id.Value);
             if (phone == null)
                 throw new ValidationException("Телефон не найден", "");

             return new PhoneDTO { Company = phone.Company, Id = phone.Id, Name = phone.Name, Price = phone.Price };
         }
        */
        public void Dispose()
        {
            dbb.Dispose();
        }
    }

}

