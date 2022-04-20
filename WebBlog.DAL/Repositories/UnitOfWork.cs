using Microsoft.EntityFrameworkCore.Infrastructure;
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
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationContext _appContext;

        private Dictionary<Type, object> _repositories;

        public UnitOfWork(ApplicationContext app)
        {
            this._appContext = app;
        }

        public void Dispose()
        {

        }

        public IRepository<TEntity> GetRepository<TEntity>(bool hasCustomRepository = true) where TEntity : class
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<Type, object>();
            }

            if (hasCustomRepository)
            {
                var customRepo = _appContext.GetService<IRepository<TEntity>>();
                if (customRepo != null)
                {
                    return customRepo;
                }
            }

            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type))
            {
                _repositories[type] = new Repository<TEntity>(_appContext);
            }

            return (IRepository<TEntity>)_repositories[type];

        }
        public int SaveChanges(bool ensureAutoHistory = false)
        {
            throw new NotImplementedException();
        }
    }

   /* public class UnitOfWork : IUnitOfWork
    {
        private ApplicationContext db;
        private RoleRepository roleRepository;
        private TagRepository tagRepository;

        public UnitOfWork()
        {
            db = new ApplicationContext();
        }
        public IRepository<Role> Roles
        {
            get
            {
                if (roleRepository == null)
                    roleRepository = new RoleRepository(db);
                return roleRepository;
            }
        }

        public IRepository<Tag> Tags
        {
            get
            {
                if (tagRepository == null)
                    tagRepository = new TagRepository(db);
                return tagRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
    /*private ApplicationContext _appContext;

    private Dictionary<Type, object> _repositories;

    public UnitOfWork()
    {
        this._appContext = new ApplicationContext();
    }

    public void Dispose()
    {

    }

    public IRepository<TEntity> GetRepository<TEntity>(bool hasCustomRepository = true) where TEntity : class
    {
        if (_repositories == null)
        {
            _repositories = new Dictionary<Type, object>();
        }

        if (hasCustomRepository)
        {
            var customRepo = _appContext.GetService<IRepository<TEntity>>();
            if (customRepo != null)
            {
                return customRepo;
            }
        }

        var type = typeof(TEntity);
        if (!_repositories.ContainsKey(type))
        {
            _repositories[type] = new Repository<TEntity>(_appContext);
        }

        return (IRepository<TEntity>)_repositories[type];

    }
    public int SaveChanges(bool ensureAutoHistory = false)
    {
        throw new NotImplementedException();
    }
}*/

}
