using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBlog.BLL.Extentions;
using WebBlog.BLL.Services;
using WebBlog.DAL.EF;
using WebBlog.DAL.Entities;
using WebBlog.DAL.Repositories;
using WebBlog.Web;

namespace API
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
            services.AddDbContext<ApplicationContext>();

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
                .AddTransient<TagService>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }

            app.UseRouting();

            app.UseAuthorization();
            // Сопоставляем маршруты с контроллерами

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
