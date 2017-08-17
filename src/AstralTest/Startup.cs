using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Swagger;
using AstralTest.Database;
using AstralTest.Domain;
using AstralTest.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using AstralTest.Identity;
using AstralTest.Extensions;
using AstralTest.FileStore;
using AstralTest.GeoLocation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace AstralTest
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            _env = env;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }
        public IHostingEnvironment _env;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<DatabaseContext>(opt =>
                opt.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")
                , x => x.MigrationsAssembly("AstralTest")));

            //Сервисы для аутификации и валидации пароля
            services.AddScoped<IHashProvider, Md5HashService>();
            services.AddScoped<IPasswordHasher<User>, Md5PasswordHasher>();

            services.AddIdentity<User, Role>()
                .AddRoleStore<RoleStore>()
                .AddUserStore<IdentityStore>()
                .AddPasswordValidator<Md5PasswordValidator>()
                .AddDefaultTokenProviders();

            if (_env.IsDevelopment())
            {
                //Временные настройки для авторизации
                services.Configure<IdentityOptions>(opt =>
                {
                    opt.Cookies.ApplicationCookie.LoginPath = "/Account/Login";
                    opt.Cookies.ApplicationCookie.LogoutPath = "/Account/Logout";

                    //Pass settings
                    opt.Password.RequiredLength = 5;
                    opt.Password.RequireDigit = false;
                    opt.Password.RequireUppercase = false;
                    opt.Password.RequireLowercase = false;
                    opt.Password.RequireNonAlphanumeric = false;
                });

                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
                });
            }
            services.AddMvc().AddMvcOptions(opt =>
                {
                    opt.Filters.Add(typeof(ErrorFilter));
                    opt.Filters.Add(typeof(LoggerUsersFilter));
                }
            );
            services.AddMemoryCache();

            //services.AddScoped<ErrorFilter>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            //Тут добавляются наши биндинги интерфейсов
            services.AddServices();
            services.AddFileStoreServices(opt =>
            {
                opt.RootPath = "C:/Users/Alexander/Desktop/AstralRepositoy";
            });
            services.AddGeoService();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseStaticFiles();
          
            app.UseIdentity();



            //Используем swagger для проверки контроллеров
            if (env.IsDevelopment())
            {
                app.UseSwagger();

                app.UseSwaggerUI(x =>
                {
                    x.SwaggerEndpoint("/swagger/v1/swagger.json", "My api");
                });
            }
            app.UseMvc(route =>
            {
                route.MapRoute("Default", "{controller=Account}/{action=Login}/{id?}");
            });
            //Тут делается миграция бд, если бд не существует
            app.ApplicationServices.GetService<DatabaseContext>().Database.Migrate();
            //Иницилизурем 2 роли и 1го пользователя, если таковых нет
            app.ApplicationServices.GetService<DatabaseContext>().Initialize(app.ApplicationServices).Wait();
        }
    }
}

