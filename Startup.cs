using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TestAwsSTS.Service;

namespace TestAwsSTS
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
            services.AddScoped<IAWSSTSTest, AWSSTSTest>();
            #region SwaggerUI
            var _swaggerTitle = "TestAwsSTS";
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = _swaggerTitle,
                    Version = "v1.0.1",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact { Name = "TestAwsSTS", Url = null }
                });
                var basePath = Directory.GetCurrentDirectory(); 
                var xmlPath = Path.Combine(basePath, "TestAwsSTS.xml");  
                options.IncludeXmlComments(xmlPath, true); 
            });

            #endregion
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwaggerUI();
            app.UseRouting();
            app.UseSwagger();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
