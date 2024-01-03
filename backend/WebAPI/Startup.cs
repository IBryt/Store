using Business.Validation;
using FluentValidation;
using FluentValidation.AspNetCore;
using IgorBryt.Store.BLL;
using IgorBryt.Store.BLL.Interfaces;
using IgorBryt.Store.BLL.Services;
using IgorBryt.Store.BLL.Validation;
using IgorBryt.Store.DAL.Data;
using IgorBryt.Store.DAL.Interfaces;
using IgorBryt.Store.DAL.Repositories;
using IgorBryt.Store.WebAPI.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace IgorBryt.Store.WebAPI;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));
        services.AddSingleton<JwtConfig>();

        services.AddControllers()
            .AddFluentValidation();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddDefaultIdentity<IdentityUser>(options =>
        {
            options.SignIn.RequireConfirmedPhoneNumber = false;
        })
        .AddEntityFrameworkStores<AppIdentityDbContext>();

        AddServices(services);
        AddRepositories(services);
        AddValidators(services);

        services.AddAutoMapper(typeof(AutoMapperProfile));

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("SqlConnectionString")));

        services.AddDbContext<AppIdentityDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("SqlConnectionString")));

        services.AddCors(options =>
        {
            options.AddPolicy("FrontEnd",
                policy => policy.WithOrigins("http://localhost:4200")
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(jwt =>
        {
            var key = Encoding.ASCII.GetBytes(Configuration.GetSection("JwtConfig:Secret").Value);
            jwt.SaveToken = true;
            jwt.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = true,
                ValidateLifetime = true,
            };
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {

        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        if (env.IsDevelopment() || env.IsProduction())
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                dbContext.Database.Migrate();

                var dbIdentityContext = scope.ServiceProvider.GetRequiredService<AppIdentityDbContext>();
                dbIdentityContext.Database.Migrate();
            }
        }

        app.UseStaticFiles();

        app.UseHttpsRedirection();

        app.UseCors("FrontEnd");
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddTransient<IProductCategoryService, ProductCategoryService>();
        services.AddTransient<IProductService, ProductService>();
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddTransient<IUnitOfWork, UnitOfWork>();

        services.AddTransient<IProductRepository, ProductRepository>();
        services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();
    }

    private static void AddValidators(IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<ProductValidator>();
        services.AddValidatorsFromAssemblyContaining<ProductCategoryValidator>();
    }
}