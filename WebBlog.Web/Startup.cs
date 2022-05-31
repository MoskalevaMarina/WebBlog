using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebBlog.BLL.Extentions;
using WebBlog.BLL.Services;
using WebBlog.DAL.EF;
using WebBlog.DAL.Entities;
using WebBlog.DAL.Interfaces;
using WebBlog.DAL.Repositories;


namespace WebBlog.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<ApplicationContext>();// (options => options.UseSqlite(connection), ServiceLifetime.Singleton);

            var mapperConfig = new MapperConfiguration((v) =>
            {
                v.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();

            services.AddSingleton(mapper);

            services
                .AddCustomRepository<Post, PostRepository>()
                .AddCustomRepository<Role, RoleRepository>()
                .AddCustomRepository<User, UserRepository>()
                .AddCustomRepository<Comment, CommentRepository>()
                .AddCustomRepository<Tag, TagRepository>()
                .AddUnitOfWork()

                .AddTransient<RoleService>()
                .AddTransient<UserService>()
                .AddTransient<CommentService>()
                .AddTransient<PostService>()
                .AddTransient<TagService>()
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
             {
               options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/User/Login");
                options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/User/Loin");
             });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler("/Home/Error");

            app.UseStaticFiles();

            app.UseRouting();
           
            app.Use(async (context, next) =>
            {
                // Строка для публикации в лог
                string logMessage = $"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}{Environment.NewLine}";

                Program.Logger.Info(logMessage);
                await next.Invoke();
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
