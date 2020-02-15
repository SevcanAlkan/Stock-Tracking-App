using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using NGA.Core;
using NGA.Data;
using NGA.Data.Helper;
using NGA.Data.Service;
using NGA.Data.SubStructure;
using NGA.Domain;
using NGA.MonolithAPI.Config;
using NGA.MonolithAPI.Fillter;
using NGA.MonolithAPI.Middleware;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;

namespace NGA.MonolithAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            StaticValues.HostAddress = (IPAddress.Loopback.ToString() + ":" + Configuration.GetValue<int>("Host:Port")).ToString();
            StaticValues.HostSSLAddress = (IPAddress.Loopback.ToString() + ":" + Configuration.GetValue<int>("Host:PortSSL")).ToString();

            string elasticUri = Configuration["ElasticConfiguration:Uri"].Replace("{HostMachineIpAddress}", GetHostMachineIP.Get());
            StaticValues.DBConnectionString = Configuration.GetConnectionString("DefaultConnection").Replace("{HostMachineIpAddress}", GetHostMachineIP.Get());

            Serilog.Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticUri))
                {
                    AutoRegisterTemplate = true,
                })
                .CreateLogger();
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

            #region Add Entity Framework and Identity Framework
            services.AddIdentity<User, Role>()
              .AddEntityFrameworkStores<NGADbContext>()
              .AddDefaultTokenProviders();
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

               //options.Events = new JwtBearerEvents
               //{
               //    OnMessageReceived = context =>
               //    {
               //        var accessToken = context.Request.Query["access_token"];

               //        var path = context.HttpContext.Request.Path;
               //        if (!string.IsNullOrEmpty(accessToken) &&
               //            (path.StartsWithSegments("/chatHub")))
               //        {
               //            context.Token = accessToken;
               //        }
               //        return Task.CompletedTask;
               //    }
               //};

               //services.AddSwaggerGen(c =>
               //{
               //    c.SwaggerDoc("v1", new Info { Title = "Values Api", Version = "v1" });
               //    c.AddSecurityDefinition("Bearer",
               //           new ApiKeyScheme
               //           {
               //               In = "header",
               //               Name = "Authorization",
               //               Type = "apiKey"
               //           });
               //    c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> {
               //     { "Bearer", Enumerable.Empty<string>() },
               //     });

               //});

           });
            #endregion

            #region MVC Configration
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(LoggerFilter));
                options.Filters.Add(typeof(ValidatorActionFilter));
            }).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });
            #endregion

            services.AddOpenApiDocument();

            #region AutoMapper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperConfig());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddAutoMapper(typeof(Startup).Assembly);
            #endregion

            #region Dependency Injection

            services.AddSingleton(mapper);
            services.AddDbContext<NGADbContext>(db => db.UseSqlServer(StaticValues.DBConnectionString));
            services.AddTransient<UnitOfWork>();
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

            services.AddTransient<IParameterService, ParameterService>();
            services.AddTransient<IGroupService, GroupService>();
            services.AddTransient<IGroupUserService, GroupUserService>();
            services.AddTransient<IMessageService, MessageService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient(typeof(IBaseService<,,,>), typeof(BaseService<,,,>));

            //services.AddSingleton<ChatHub>();

            #endregion

            #region Add SignalR
            //services.AddSignalR().AddHubOptions<ChatHub>(options =>
            //{
            //    options.EnableDetailedErrors = true;
            //});
            #endregion

            #region Versioning

            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(2, 0);
                //o.ApiVersionReader = ApiVersionReader.Combine(new QueryStringApiVersionReader(),
                //    new HeaderApiVersionReader("api-version"));
            });

            #endregion

            #region Swagger

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("1", new OpenApiInfo() { Title = "Next Generation API", Version = "1" });
                s.SwaggerDoc("2", new OpenApiInfo() { Title = "Next Generation API", Version = "2" });

                // Apply the filters
                s.OperationFilter<RemoveVersionFromParameter>();
                s.DocumentFilter<ReplaceVersionWithExactValueInPath>();



                s.DocInclusionPredicate((docName, apiDesc) =>
                {
                    if (!apiDesc.TryGetMethodInfo(out MethodInfo methodInfo)) return false;

                    var versions = methodInfo.DeclaringType
                        .GetCustomAttributes(true)
                        .OfType<ApiVersionAttribute>()
                        .SelectMany(attr => attr.Versions);

                    return versions.Any(v => $"v{v.ToString()}" == docName);
                });
            });
            //services.AddSwaggerGenNewtonsoftSupport();

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, NGADbContext dbContext, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            ParameterHelperLoder.LoadStaticValues(dbContext);
            DbInitializer.Initialize(dbContext);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            loggerFactory.AddSerilog();

            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/1/swagger.json", "NGA V1");
                s.SwaggerEndpoint("/swagger/2/swagger.json", "NGA V2");

                s.RoutePrefix = string.Empty;
            });

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseDefaultFiles();
            app.UseStaticFiles();

            //app.UseSignalR(routes =>
            //{
            //    routes.MapHub<ChatHub>("/chathub");  //https://stackoverflow.com/questions/43181561/signalr-in-asp-net-core-1-1
            //});

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(myCorsPolicy);
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseOpenApi();
            app.UseSwaggerUi3();
        }
    }
}
