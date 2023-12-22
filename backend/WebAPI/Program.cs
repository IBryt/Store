using Business.Validation;
using FluentValidation;
using IgorBryt.Store.BLL;
using IgorBryt.Store.BLL.Interfaces;
using IgorBryt.Store.BLL.Models;
using IgorBryt.Store.BLL.Services;
using IgorBryt.Store.BLL.Validation;
using IgorBryt.Store.DAL.Data;
using IgorBryt.Store.DAL.Interfaces;
using IgorBryt.Store.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

AddServices(builder.Services);
AddRepositories(builder.Services);
AddValidators(builder.Services);

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnectionString")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
        builder => builder.WithOrigins("http://localhost:4200"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        dbContext.Database.Migrate();
    }
}

app.UseCors("AllowOrigin");

app.UseStaticFiles();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

void AddServices(IServiceCollection services)
{
    services.AddTransient<IProductCategoryService, ProductCategoryService>();
    services.AddTransient<IProductService, ProductService>();
}

void AddRepositories(IServiceCollection services)
{
    services.AddTransient<IUnitOfWork, UnitOfWork>();

    services.AddTransient<IProductRepository, ProductRepository>();
    services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();
}

void AddValidators(IServiceCollection services)
{
    services.AddTransient<IValidator<ProductCategoryModel>, ProductCategoryValidator>();
    services.AddTransient<IValidator<ProductModel>, ProductValidator>();
}
