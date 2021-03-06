using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using projectStructure.BLL.MappingProfiles;
using projectStructure.BLL.Services;
using projectStructure.DAL;
using Microsoft.EntityFrameworkCore;
using projectStructure.DAL.Repositories;

namespace projectStructure.WebAPI
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
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<UserProfile>();
                cfg.AddProfile<ProjectProfile>();
                cfg.AddProfile<TasksProfile>();
                cfg.AddProfile<TeamProfile>();
            },
            Assembly.GetExecutingAssembly());

            services.AddTransient<IRepository<Project>, BaseRepository<Project>>();
            services.AddTransient<IRepository<User>, BaseRepository<User>>();
            services.AddTransient<IRepository<Team>, BaseRepository<Team>>();
            services.AddTransient<IRepository<Tasks>, BaseRepository<Tasks>>();

            services.AddScoped<ProjectService>();
            services.AddScoped<LinqService>();
            services.AddScoped<TeamService>();
            services.AddScoped<TasksService>();
            services.AddScoped<UserService>();
            services.AddDbContext<ProjectsDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ProjectsDatabase")));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "projectStructure", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "projectStructure v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
