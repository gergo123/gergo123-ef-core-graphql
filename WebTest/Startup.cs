using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Options;
using AutoMapper;
using GraphQL;
using GraphQL.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GraphQL.Types;
using WebTest.GraphQl;
using WebTest.Model;
using WebTest.Web;
using DbTest.Model;
using DbTest.RLS;
using DbTest.Repositories.RLS;
using DbTest.Repositories;
using DbTest.SecureRepository.PlaceHolder;
using DbTest.Repositories.GeneralRepository.Stepper;
using System.Text.RegularExpressions;

namespace WebTest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private readonly string DevelopmentPolicy = "_DevelopmentPolicy";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.Configure<AppConfiguration>(Configuration.GetSection("AppConfiguration"));

            services.AddAuthentication(IISDefaults.AuthenticationScheme);
            services.AddCors();

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions((opt) =>
                {
                    // Needed for React admin controller to work
                    opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "../ClientSideProjectFolder/build";
            });

            // Add framework services.
            services.AddAutoMapper(typeof(Startup));

            //services.AddScoped(_ => new ConnectionStringProvider(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<CoreContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<CurrentUserProvider>();
            services.AddScoped<IUserSecurityObjectsHandler, UserSecurityObjectsHandler>();

            // Security stuff
            services.AddScoped<ISecurityObjectRepository, SecurityObjectRepository>();
            services.AddScoped<PermissionService>();

            services.AddScoped<ISimplePlaceholderRepository, SimplePlaceholderRepository>();
            services.AddScoped<IPlaceholderSecureRepository, PlaceholderSecureRepository>();
            services.AddScoped<IPlaceholderACLRepository, PlaceholderACLRepository>();

            services.AddScoped<ITestEntityRepository, TestEntityRepository>();

            //services.AddScoped<IBasicTaskRepository, BasicTaskRepository>();
            //services.AddScoped<IBasicTaskAclRepository, BasicTaskAclRepository>();
            //services.AddScoped<IBasicTaskSecureRepository, BasicTaskSecureRepository>();

            // State change
            //services.AddScoped<StateChangeFactory>();
            //services.AddScoped<IStateManagger, StateManagger>();

            // GraphQl
            services.AddHttpContextAccessor();
            services.AddSingleton<ContextServiceLocator>();
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<IDocumentWriter, DocumentWriter>();
            services.AddSingleton<ModelQuery>();
            services.AddSingleton<ModelMutation>();
            services.AddSingleton<TestGraphQlModel>();

            var sp = services.BuildServiceProvider();
            services.AddSingleton<ISchema>(new ModelSchema(new FuncDependencyResolver(type => sp.GetService(type))));

            services.AddCors(options =>
            {
                options.AddPolicy(DevelopmentPolicy,
                builder =>
                {
                    builder.WithOrigins("http://localhost:37680") // client side url, check logs for port number on startup
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            IOptionsMonitor<AppConfiguration> appConfig)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //app.UseCookiePolicy();

            if (env.IsDevelopment())
            {
                app.UseCors(DevelopmentPolicy);
            }

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseWhen((context) =>
            {
                var path = context.Request.Path;
                // Dont use security middleware for statis files, css, js etc. to avoid unneccessary db access if not required
                var regexp = new Regex("\\.(gif|jpe?g|tiff|png|webp|bmp|js|css)$");
                var toUse = !regexp.IsMatch(path);

                return toUse;
            }, (config) => config.UseMiddleware<SecurityMiddleware>());

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.Map("/schema.graphql", config => config.UseMiddleware<SchemaMiddleware>());

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "../ClientSideProjectFolder";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
