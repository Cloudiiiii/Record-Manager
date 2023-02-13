using Microsoft.EntityFrameworkCore;
using YHAssignment3.DataAccess;
using YHAssignment3.Services;
using Vendors.Services;

var builder = WebApplication.CreateBuilder(args);

var connStr = builder.Configuration.GetConnectionString("A3DB");
builder.Services.AddDbContext<VendorDbContext>(options => options.UseSqlServer(connStr));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IVendorManager, VendorManager>();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
