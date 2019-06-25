using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using SiteCrete.Server.API.Client.Repositories;
using SiteCrete.Server.API.Client.Interfaces;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.HttpOverrides;
using SiteCrete.Server.API.Extensions;
using System.Globalization;

namespace SiteCrete.Server.API
{
    public class Startup
    {
        private const string corsPolicy = "CorsPolicy";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
                            {
                                options.AddPolicy(corsPolicy,
                                builder =>
                                {
                                    builder.WithOrigins(Configuration.GetValue<string>("ClientUrl"))
                                                        .AllowAnyHeader()
                                                        .AllowAnyMethod();
                                });
                            });

            services.AddSingleton<IConfiguration>(Configuration);
            services.AddSingleton<IDbConnectionFactory>(new OrmLiteConnectionFactory(Configuration.GetConnectionString("DefaultConnection"), MySqlDialect.Provider));
            services.AddSingleton<IPictureRepository, PictureRepository>();
            services.AddSingleton<ICategoryRepository, CategoryRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IReservationRepository, ReservationRepository>();
            services.AddSingleton<ITextRepository, TextRepository>();
            services.AddSingleton<ILinkRepository, LinkRepository>();
            services.AddSingleton<IGoldCommentRepository, GoldCommentRepository>();
            services.AddSingleton<IDiscoverRepository, DiscoverRepository>();
            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.Configure<FormOptions>(x => {
                x.MemoryBufferThreshold = int.MaxValue;
                x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = int.MaxValue; // In case of multipart
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHttpsRedirection();
                app.UseHsts();
            }
            var cultureInfo = new CultureInfo("fr-BE");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            app.UseCors(corsPolicy);
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
                RequestPath = new PathString("/Resources")
            });

            app.ConfigureExceptionHandler();
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });
            app.UseMvc();
        }
    }
}
