using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBlog.DAL.Interfaces;
using WebBlog.DAL.Repositories;

namespace WebBlog.BLL.Extentions
{
    public static class ServiceExtentions
    {
        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }

        public static IServiceCollection AddCustomRepository<TEntity, IRepository>(this IServiceCollection services)
                 where TEntity : class
                 where IRepository : class, IRepository<TEntity>
        {
            services.AddScoped<IRepository<TEntity>, IRepository>();
            return services;
        }

    }

}
