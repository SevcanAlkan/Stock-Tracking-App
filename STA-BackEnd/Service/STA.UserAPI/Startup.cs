using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using STA.Core.Filter;
using STA.Core.Middleware;
using STA.UserAPI.Config;
using System;
using System.IO;
using System.Reflection;

namespace STA.UserAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        readonly string myCorsPolicy = "apiCorsPolicy";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Add CORS
            services.AddCors(options =>
                options.AddPolicy(myCorsPolicy, builder =>
                {
                    builder
                        .WithOrigins(new[] { "http://localhost:4200" })
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                }));
            #endregion

            #region MVC Configuration
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(ValidatorActionFilter));
            }).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });
            #endregion

            #region Swagger

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "User API",
                    Version = "v1",
                    Description = "Stock Tracking Application User API",
                    Contact = new OpenApiContact
                    {
                        Name = "Sevcan ALKAN",
                        Email = "sevcanalkan@outlook.com.tr",
                        Url = new Uri("https://github.com/SevcanAlkan"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under MIT",
                        Url = new Uri("https://github.com/SevcanAlkan/Stock-Tracking-App/blob/master/LICENSE"),
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            #endregion

            #region AutoMapper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperConfig());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddAutoMapper(typeof(Startup).Assembly);
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

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "User API");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();
            app.UseCors(myCorsPolicy);
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
