using lab09.Interfaces;
using lab09.Services;

var builder = WebApplication.CreateBuilder(args);

// --- SEKCJA KONFIGURACJI SERWISÓW (Services) ---

// Add services to the container.
builder.Services.AddControllersWithViews();

// 2. REJESTRACJA INTERFEJSU W KONTEKŒCIE DI
//
// W tym miejscu decydujesz, której implementacji u¿yæ. 
// Poniewa¿ kolekcje s¹ w pamiêci i chcemy, aby dane by³y zachowane
// przez ca³y czas dzia³ania aplikacji, u¿ywamy AddSingleton.

// Wybierz jedn¹ z poni¿szych linii (pozosta³e zakomentuj):

// Rejestracja z u¿yciem List<Article>:
builder.Services.AddSingleton<IArticlesContext, ListArticlesContext>();

// Rejestracja z u¿yciem Dictionary<int, Article>:
//builder.Services.AddSingleton<IArticlesContext, DictionaryArticlesContext>();


// ----------------------------------------------------

var app = builder.Build();

// --- SEKCJA KONFIGURACJI REQUEST PIPELINE (Middleware) ---

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Domyœlnie dla statycznych plików (CSS, JS)

app.UseRouting();

app.UseAuthorization();

// 3. Konfiguracja Routingu dla Kontrolerów (pozostaje bez zmian)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();