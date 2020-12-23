using API_Template.Controllers;
using DbCommand;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Tempalate.Business.Implements;
using Tempalate.Business.Interface;

namespace API_Template
{
    public class Startup
    {
        public const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IMemoryCache Cache { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                {
                    // Return JSON responses in LowerCase?
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    // Resolve Looping navigation properties
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ToDo API",
                    Description = "A simple example ASP.NET Core Web API",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Shayne Boyer",
                        Email = string.Empty,
                        Url = new Uri("https://twitter.com/spboyer"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/license"),
                    }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

            });

            services.AddControllers();


            services.AddAuthentication()
                .AddJwtBearer(cfg =>
                {
                    //cfg.RequireHttpsMetadata = false;
                    //cfg.SaveToken = true;
                    cfg.TokenValidationParameters = TokensController.GetTokenValidationParameters(Configuration, true);
                    cfg.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = async context =>
                        {
                            var jwtToken = (JwtSecurityToken)context.SecurityToken;
                            var userId = TokensController.GetClaimsUserId(jwtToken.Claims);
                            var device = TokensController.GetClaimsDevice(jwtToken.Claims);

                            var accessToken = TokensController.GetCacheAccessTokenAsync(Cache, userId, device);

                            if (context.SecurityToken.Id != accessToken)
                            {
                                var refreshToken = TokensController.GetCacheRefreshTokenAsync(Cache, userId, device);

                                if (string.IsNullOrWhiteSpace(refreshToken))
                                {
                                    context.Response.Headers.Add("Token-Revoked", "Access-Refresh");
                                    context.Fail("Token-Revoked-Access-Refresh");
                                }
                                else
                                {
                                    context.Response.Headers.Add("Token-Revoked", "Access");
                                    context.Fail("Token-Revoked-Access");
                                }
                            }

                            return;
                        },
                        OnAuthenticationFailed = async context =>
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {

                                if (context.Principal != null)
                                {
                                    context.Response.Headers.Add("Token-Expired", "Access");
                                    var userId = TokensController.GetClaimsUserId(context.Principal.Claims);
                                    var device = TokensController.GetClaimsDevice(context.Principal.Claims);


                                    var refreshToken = TokensController.GetCacheRefreshTokenAsync(Cache, userId, device);

                                    if (string.IsNullOrWhiteSpace(refreshToken))
                                    {
                                        context.Response.Headers.Add("Token-Revoked", "Refresh");
                                    }
                                }

                            }
                        }
                    };
                });
            ;

            services.AddMemoryCache();


            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder => builder
                           .AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod()

                );
            });

            //services.AddSingleton<IValidator<MenuFunctionDmCreatetInputOrEdit>, MenuFunctionCreatetOrEditVd>();


            //// mvc + validating
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0).AddFluentValidation();

            // override modelstate
            services.Configure<ApiBehaviorOptions>(options =>
                {
                    options.InvalidModelStateResponseFactory = (context) =>
                    {
                        var errors = context.ModelState.Values.SelectMany(x => x.Errors.Select(p => p.ErrorMessage)).ToList();
                        var result = new
                        {
                            Code = "400",
                            Message = "Validation errors",
                            Errors = errors
                        };
                        return new BadRequestObjectResult(result);
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMemoryCache cache, ILogger<Startup> logger)
        {
            Cache = cache;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseStatusCodePagesWithRedirects("/404Page/{0}");
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });


            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
