using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitiesWebApi.DataContext;
using CitiesWebApi.Model;
using CitiesWebApi.Model.DTO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace CitiesWebApi
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
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CityDTO, City>();
                cfg.CreateMap<CityWithAttractionsDTO, City>();
            });

            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);
            
            
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddXmlSerializerFormatters();
            services.AddDbContext<CityDataContext>(options =>
            {
                options.UseInMemoryDatabase("Jan");
                
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Cities", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseMvc();
            app.UseSwagger();           
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cities V1");
                c.RoutePrefix = string.Empty;
            });
          app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello Din Mor!");
            });
        }
    }
}
