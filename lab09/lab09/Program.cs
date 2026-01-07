using lab09.Interfaces;
using lab09.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();


// Rejestracja z u¿yciem List<Article>:
builder.Services.AddSingleton<IArticlesContext, ListArticlesContext>();

// Rejestracja z u¿yciem Dictionary<int, Article>:
//builder.Services.AddSingleton<IArticlesContext, DictionaryArticlesContext>();


// ----------------------------------------------------

var app = builder.Build();



if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

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