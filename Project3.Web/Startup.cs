using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Project3.Data.Data;
using Project3.Data.Service;
using Project3.Data.Service.Interface;
using Project3.Extensions;
using Project3.Web.Data;
using Project3.Web.Service;
using Project3.Web.Service.Interface;

namespace Project3.Web
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

            services.AddMemoryCache();
            services.AddSession();
            services.AddMvc();

            services.AddWebEncoders(opt =>
            {
                opt.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All);

            });
            //身份验证
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;//方案名称
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,//cookie名称
                o =>
                {
                    o.Cookie.HttpOnly = true;//禁止客户端JS读取cookie
                    o.LoginPath = new PathString("/Admin/Default/Login");//登录页面

                });


            services.AddDbContextPool<Project3DbContext>(options =>
            {
                var dbType = Configuration.GetValue<string>("DbType");

                if (string.Equals(dbType, "sqlserver", StringComparison.OrdinalIgnoreCase))
                {
                    options.UseSqlServer(Configuration.GetConnectionString("SQLServer"));
                }
                else if (string.Equals(dbType, "sqlite", StringComparison.OrdinalIgnoreCase))
                {
                    options.UseSqlite(Configuration.GetConnectionString("SQLite"));
                }
                else
                {
                    throw new Exception("appsettings.json:DbType->type not found!");
                }
            });
            services.AddScoped<DbContext, Project3DbContext>();
            services.AddScoped<Project3DB>();
            services.AddScoped<ICache, Cache>();

            //内容管理服务
            services.AddTransient<IContentManagerService, ContentManagerService>();
            //标签分类管理服务
            services.AddTransient<IMetasManagerService, MetasManagerService>();
            //文件管理服务
            services.AddTransient<IFilesManagerService, FilesManagerService>();
            //评论管理服务
            services.AddTransient<ICommentManagerService, CommentManagerService>();
            //设置管理服务
            services.AddTransient<IOptionsManagerService, OptionsManagerService>();
            //用户管理服务
            services.AddTransient<IUsersManagerService, UserManagerService>();


            //内容扩展
            services.AddTransient<ContentExtension>();

            //数据初始化服务
            services.AddTransient<IDbInitializer, DbInitializer>();
            //站点配置 缓存服务
            services.AddTransient<IOptionsCache, OptionsCache>();
            //页面 缓存服务
            services.AddTransient<IPageCache, PageCache>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IDbInitializer dbInitializer)
        {
            //数据库初始发
            dbInitializer.Initialize();
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseSession();


            //反向代理
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            //身份验证
            app.UseAuthentication();


            app.UseMvc(routes =>
            {
                routes.MapRoute(
            name: "areas",
            template: "{area:exists}/{controller=Default}/{action=Index}/{id?}"
          );
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Default}/{action=Index}/{id?}");
            });



        }
    }
}
