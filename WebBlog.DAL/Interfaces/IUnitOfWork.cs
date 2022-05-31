using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBlog.DAL.Entities;

namespace WebBlog.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        int SaveChanges(bool ensureAutoHistory = false);

        IRepository<TEntity> GetRepository<TEntity>(bool hasCustomRepository = true) where TEntity : class;    
    }
}
