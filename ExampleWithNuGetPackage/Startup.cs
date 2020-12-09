using ENP.DA;
using ExampleWithNuGetPackage.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ShivOhm.Infrastructure;
using System;

namespace ExampleWithNuGetPackage
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
            services.AddControllers();
            services.RootServicesRegistered(Configuration.GetConnectionString("TestDefaultConnection")); // From infra
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Generic Code Demo", Version = "v1" });
            });

            services.AddDbContext<GenericDbContext>(options =>
                        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddMvc()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.IgnoreNullValues = true;
            });

            services.AddMyServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILog logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.ConfigureApplication(logger, true, 1); // from infra
            bool AutoRun = Convert.ToBoolean(Configuration.GetSection("Migrator").GetSection("AutoRun").Value);
            InfraEnums.MigrationType MigrationType = (InfraEnums.MigrationType)Enum.Parse(typeof(InfraEnums.MigrationType), Configuration.GetSection("Migrator").GetSection("MigratorType").Value, true);
            long? DownVersion = !string.IsNullOrWhiteSpace(Configuration.GetSection("Migrator").GetSection("DownVersion").Value) ? Convert.ToInt64(Configuration.GetSection("Migrator").GetSection("DownVersion").Value) : default;

            app.ConfigureApplication(logger, AutoRun, MigrationType, DownVersion); // from infra
        }
    }
}
