using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;
using N.Model.Entities;
using N.Service.Core.AutoMapper;
using N.Service.Common.Service;
using N.Repository;
using N.Model;
using N.Extensions;
using Microsoft.OpenApi.Models;

namespace N
{
    public static class Startup
    {

        public static void UseConfigurationServices(this IServiceCollection services)
        {
            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });
            services.AddSwaggerGen(opts =>
            {
                opts.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                opts.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type =ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme,

                            }
                        }, new string[]{ } 
                    }
                });
            });
            //services.AddSignalR();
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

            services.Configure<FormOptions>(options => { options.MultipartBodyLengthLimit = 1048576000; });

            services.AddDbContext<AppDBContext>(options =>
             {
                 var connectionString = AppSettings.ConnectionStrings.DefaultConnection;
                 options.UseSqlServer(connectionString, b => b.MigrationsAssembly("N.Model"));
             });

            services.AddDependencyInjection();

            services.AddIdentity<AppUser, AppRole>()
                 .AddEntityFrameworkStores<AppDBContext>()
                 .AddDefaultTokenProviders();
            services.AddAuthentication(options =>
             {
                 options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                 options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
             }).AddJwtBearer(options =>
             {
                 options.TokenValidationParameters = new TokenValidationParameters()
                 {
                     ValidateIssuer = false,
                     ValidateAudience = false,
                     //ValidAudience = builder.Configuration.GetSetting().AuthSettings.Audience,
                     //ValidIssuer = builder.Configuration.GetSetting().AuthSettings.Issuer,
                     RequireExpirationTime = true,
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettings.AuthSettings.Key)),
                     ValidateIssuerSigningKey = true,
                 };
             });
            services.AddDistributedSqlServerCache(options =>
             {
                 options.ConnectionString = AppSettings.ConnectionStrings.DistCacheConnectionString;
                 options.SchemaName = "dbo";
                 options.TableName = "DistributedSqlServerCache";
             });

            services.Configure<IdentityOptions>(options =>
             {
                 // Password settings.
                 options.Password.RequireDigit = false;
                 options.Password.RequireLowercase = false;
                 options.Password.RequireNonAlphanumeric = false;
                 options.Password.RequireUppercase = false;
                 options.Password.RequiredLength = 6;
                 options.Password.RequiredUniqueChars = 0;

                 // Lockout settings.
                 options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(AppSettings.AuthSettings.SecondsExpires);
                 options.Lockout.MaxFailedAccessAttempts = 5;
                 options.Lockout.AllowedForNewUsers = true;

                 // User settings.
                 options.User.AllowedUserNameCharacters =
                 "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                 options.User.RequireUniqueEmail = true;

                 //SignIn settings
                 options.SignIn.RequireConfirmedEmail = true;
             });

            services.ConfigureApplicationCookie(options =>
             {
                 // Cookie settings
                 options.Cookie.HttpOnly = true;
                 options.ExpireTimeSpan = TimeSpan.FromSeconds(AppSettings.AuthSettings.SecondsExpires);
                 options.LoginPath = "/Identity/Account/Login";
                 options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                 options.SlidingExpiration = true;
             });

            //services.AddAuthentication()
            //     .AddGoogle(options =>
            //     {
            //         options.ClientId = AppSettings.ExternalAuth.GoogleAuth.ClientId;
            //         options.ClientSecret = AppSettings.ExternalAuth.GoogleAuth.ClientSecret;
            //         options.CallbackPath = "/google";
            //     });
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin() // Allow any origin
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });
        }

        private static void AddDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<DbContext, AppDBContext>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            var repositoryTypes = typeof(IRepository<>).Assembly.GetTypes()
                 .Where(x => !string.IsNullOrEmpty(x.Namespace) && x.Namespace.StartsWith("N.Repository") && x.Name.EndsWith("Repository"));
            foreach (var intf in repositoryTypes.Where(t => t.IsInterface))
            {
                var impl = repositoryTypes.FirstOrDefault(c => c.IsClass && intf.Name.Substring(1) == c.Name);
                if (impl != null) services.AddScoped(intf, impl);
            }

            services.AddScoped(typeof(IService<>), typeof(Service<>));
            var serviceTypes = typeof(IService<>).Assembly.GetTypes()
                 .Where(x => !string.IsNullOrEmpty(x.Namespace) && x.Namespace.StartsWith("N.Service") && x.Name.EndsWith("Service"));
            foreach (var intf in serviceTypes.Where(t => t.IsInterface))
            {
                var impl = serviceTypes.FirstOrDefault(c => c.IsClass && intf.Name.Substring(1) == c.Name);
                if (impl != null) services.AddScoped(intf, impl);
            }
        }
    }

}
