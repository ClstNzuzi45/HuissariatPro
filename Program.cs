using HuissariatPro.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
//builder.Services.AddDbContext<ApplicationDbContext>(o =>
//    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<ApplicationDbContext>(o =>
    o.UseSqlite("Data Source=huissariatpro.db"));

// Session pour le formulaire de contact (optionnel)
builder.Services.AddSession(o => { o.IdleTimeout = TimeSpan.FromMinutes(30); });

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Site/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseSession();

// Route par défaut = SITE PUBLIC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Site}/{action=Index}/{id?}");

app.Run();