using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace RestItemService
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
            //Cors. Bruges til at definere hvem der har adgang til hvad.
            services.AddCors(options =>
            {
                options.AddPolicy("NicoItems", builder => 
                    builder.WithMethods("Get", "Post", "Put", "Delete", "Patch", "Option").AllowAnyHeader().AllowAnyOrigin());
            });
            //Swagger: Bruges dokumentation af Min API, 
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("mySwagger", new OpenApiInfo {Title = "Items API", Version = "v1.0"});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseRouting();
            app.UseCors("NicoItems");
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/mySwagger/swagger.json", "Items API v1.0");
                c.RoutePrefix = "api/help";
            });
        }
    }
}
