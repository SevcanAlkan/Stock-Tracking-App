using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using STA.Core.Filter;
using STA.Core.Middleware;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace STA.AuthenticationAPI
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

            #region Add Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(options =>
           {
               var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetValue<String>("Jwt:Key")));

               options.RequireHttpsMetadata = false;
               options.SaveToken = true;

               options.TokenValidationParameters = new TokenValidationParameters
               {
                   LifetimeValidator = (before, expires, token, param) =>
                   {
                       return expires > DateTime.UtcNow;
                   },
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = signingKey,
                   ValidAudience = Configuration["Jwt:Audience"],
                   ValidIssuer = Configuration["Jwt:Issuer"],
               };

           });
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

            #region

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {
                    Title = "Authentication API",
                    Version = "v1",
                    Description = "Stock Tracking Application Authentication API",
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

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Authentication API");
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
