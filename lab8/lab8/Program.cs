using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Dodaj pamiêæ rozproszon¹ i sesjê
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
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
app.UseRouting();

// W³¹cz sesjê (miêdzy UseRouting a UseAuthorization)
app.UseSession();

app.UseAuthorization();

app.MapStaticAssets();

//app.MapControllerRoute(
//    name: "solveQuadratic",
//    pattern: "Tool/Solve/{a}/{b}/{c}",
//    defaults: new { controller = "Tool", action = "Solve" })
//    .WithStaticAssets();

app.MapControllerRoute(
    name: "gameSet",
    pattern: "Set,{n}",
    defaults: new { controller = "Game", action = "Set" })
    .WithStaticAssets();

app.MapControllerRoute(
    name: "gameDraw",
    pattern: "Draw",
    defaults: new { controller = "Game", action = "Draw" })
    .WithStaticAssets();

app.MapControllerRoute(
    name: "gameGuess",
    pattern: "Guess,{guess}",
    defaults: new { controller = "Game", action = "Guess" })
    .WithStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
