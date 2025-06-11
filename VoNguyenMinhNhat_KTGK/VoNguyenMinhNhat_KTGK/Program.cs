using VoNguyenMinhNhat_KTGK.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http; // 👈 để dùng session
using Microsoft.AspNetCore.Session;

var builder = WebApplication.CreateBuilder(args);

// ✨ Thêm Session service
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor(); // cho phép dùng session trong view

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Test1")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// ✨ Thêm UseSession trước UseAuthorization
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=SinhViens}/{action=Index}/{id?}");

app.Run();
