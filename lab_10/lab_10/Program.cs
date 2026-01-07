using Microsoft.EntityFrameworkCore;
using lab_10.Data;

var builder = WebApplication.CreateBuilder(args);

// Dodaj obs³ugê kontrolerów i widoków
builder.Services.AddControllersWithViews();

// REJESTRACJA BAZY DANYCH
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Konfiguracja potoku HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // To pozwala serwowaæ obrazki z wwwroot

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();