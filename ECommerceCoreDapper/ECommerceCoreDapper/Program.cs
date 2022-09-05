using ECommerceCoreDapper.Models;
using ECommerceCoreDapper.Models.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IGenericRepository<Products>, ProductRepository>();
builder.Services.AddScoped<IGenericRepository<Suppliers>, SupplierRepository>();
builder.Services.AddScoped<IGenericRepository<CreditCards>, CreditCardRepository>();
builder.Services.AddScoped<IGenericRepository<Customers>, CustomerRepository>();
builder.Services.AddScoped<IGenericRepository<Categories>, CategoryRepository>();
builder.Services.AddScoped<IGenericRepository<Suppliers>, SupplierRepository>();
builder.Services.AddScoped<IGenericRepository<Address>, AddressRepository>();
builder.Services.AddSession();
builder.Services.AddDbContextPool<ECommerceDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ECommerceDBCon"));
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddControllers().AddJsonOptions(jsonOptions =>
{
    jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = null;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.UseCookiePolicy();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
