using Core.Entities;
using DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

//Services
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
var connectString = builder.Configuration["ConnectionStrings:Default"];
builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer("Server=DESKTOP-92H8HCG;Database=JuanDb;Trusted_Connection=true");
});
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireDigit= true;
    options.Password.RequireLowercase= true;
    options.Password.RequireUppercase= true;
    options.User.RequireUniqueEmail= true;
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
}).AddEntityFrameworkStores<AppDbContext>();//Store olaraq bu db context-i isdifade elesin

//http request
var app = builder.Build();

app.UseAuthentication();//Autentifikasiya elesin yeni ilk olaraq yaratsin 
app.UseAuthorization();//yaranmis user-in datalarina erisilsin
app.MapControllerRoute(
           name: "areas",
           pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
         );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseStaticFiles();

app.Run();

