using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitBrainsBlogApi.Handlers;
using GitBrainsBlogApi.Models;
using GitBrainsBlogApi.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace GitBrainsBlogApi
{
    public class Startup
    {
        public static string SQLiteConnectionString { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //учим понимать JObject на входе в метода контроллера http-post
            services.AddControllers().AddNewtonsoftJson();

            //добавляем обработчик всех экспешнов в апишке
            services.AddMvc(options =>
                {
                    options.Filters.Add(typeof(ExceptionHandler));
                });

            //получаем строку подключения к бд из файла конфигурации appSettings
            SQLiteConnectionString = Configuration.GetConnectionString("DefaultConnection");

            //добавляем jwt аутентификацю
            var key = Encoding.ASCII.GetBytes("superSecretKey@345");
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            //добавляем прослушки для получения в одном месте клаймов авторизованного юзера
            services.AddHttpContextAccessor();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddSingleton<Account>();

            //добавляем репозитории для вызова в конструкторах контроллера
            services.AddSingleton<PostRepository>();
            services.AddSingleton<RoleRepository>();
            services.AddSingleton<TagRepository>();
            services.AddSingleton<PostTagRepository>();
            services.AddSingleton<CategoryRepository>();
            services.AddSingleton<CategoryPostRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //разрешаем крос доменные запросы с локалхоста
            app.UseCors(corsPolicyBuilder =>
              corsPolicyBuilder.WithOrigins("http://localhost:4200")
             .AllowAnyMethod()
             .AllowAnyHeader()
           );

            
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);


            app.UseDefaultFiles();
            app.UseStaticFiles();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
