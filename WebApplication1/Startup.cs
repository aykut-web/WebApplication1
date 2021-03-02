using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using WebApplication1.ApplicationLayer.AutoMapper;
using WebApplication1.ApplicationLayer.Mapper.Infrastructure;
using WebApplication1.ApplicationLayer.Mapper.Repositories;
using WebApplication1.ApplicationLayer.Services.Abstraction;
using WebApplication1.ApplicationLayer.Services.Concrete;
using WebApplication1.DomainLayer.Entities.Concrete;
using WebApplication1.InfrastructureLayer.Context;
using WebApplication1.InfrastructureLayer.Repositories.Kernel;

namespace WebApplication1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ProjectContext>();
            services.AddScoped(typeof(IMovieRepository), typeof(MovieService));

            services.AddScoped(typeof(ILocationRepository), typeof(LocationService));

            services.AddScoped(typeof(IMappedMovie), typeof(MappedMovie));
            services.AddScoped(typeof(IMappedLocation), typeof(MappedLocation));

            services.AddScoped(typeof(IRepositories<>), typeof(CoreService<>));

            

            

            var configuration = new AutoMapper.MapperConfiguration(config =>
            {
                config.AddProfile(new Mapping());
            });


            var mapper = configuration.CreateMapper();
            services.AddSingleton(mapper);


            //services.AddDbContext<ProjectContext>(option => option.UseSqlServer(Configuration.GetConnectionString("Default")));
            

            services.AddDbContext<ProjectContext>(options => options.UseSqlServer
            ("Server=DESKTOP-GFEA6DE;Database=WebApplication1;UID=sa;PWD=123;", x => x.MigrationsAssembly("WebApplication1.PresentationLayer")));



            services.AddControllersWithViews();
            services.AddIdentity<User, IdentityRole>(options =>
            {
                
                //options.User.AllowedUserNameCharacters = "qwertyuopilkjhgfdsazxcvbnm123456789_";

               
                //options.Password.RequiredLength = 5;

               
                options.User.RequireUniqueEmail = true;

                
                //options.Password.RequireDigit = true;

                
                //options.Password.RequireUppercase = true;

              
                //options.Password.RequireLowercase = true;

            }).AddEntityFrameworkStores<ProjectContext>()
                                      .AddDefaultTokenProviders();

            
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

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
