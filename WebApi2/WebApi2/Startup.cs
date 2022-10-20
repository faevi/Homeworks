using WebApi2.Context;
using Microsoft.EntityFrameworkCore;
using WebApi2.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Xml.Linq;
using Microsoft.Extensions.Configuration;

namespace WebApi2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var conStrBuilder = new SqlConnectionStringBuilder(Configuration.GetConnectionString("DefaultConnection"));
            conStrBuilder.Password = Configuration["DbPassword"];
            var connection = conStrBuilder.ConnectionString;

            services.AddSwaggerGen();
            services.AddDbContext<UsersContext>(options => options.UseSqlServer(connection));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.SaveToken = true;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = AuthOptions.ISSUER,
                            ValidateAudience = true,
                            ValidAudience = AuthOptions.AUDIENCE,
                            ValidateLifetime = true,
                            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                            ValidateIssuerSigningKey = true,
                        };
                    });
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            services.AddControllers();
            services.AddCors();
            services.AddEndpointsApiExplorer();
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors(x => x  
                .WithOrigins("https://localhost:7097") 
                .AllowCredentials()
                .AllowAnyMethod()
                .AllowAnyHeader()
                );
            app.UseCookiePolicy(new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Strict,
                HttpOnly = HttpOnlyPolicy.Always,
                Secure = CookieSecurePolicy.Always
            });
            app.Use(async (context, next) =>
            {
                var token = context.Request.Cookies[".AspNetCore.Application.Id"];
                if (!string.IsNullOrEmpty(token))
                    context.Request.Headers.Add("Authorization", "Bearer " + token);

                await next();
            });
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

