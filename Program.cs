using Internationalization.Providers;
using Internationalization.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Options;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddLocalization(options =>
{
    options.ResourcesPath = "Resources";
});
builder.Services.AddControllersWithViews()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();
builder.Services.AddSingleton<EmployeesDataService>();

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

app.UseRequestLocalization(options =>
{
    var culture = new List<CultureInfo> { // supported cultures
        new CultureInfo("en-en"),
        new CultureInfo("fr-fr"),
        new CultureInfo("de-de")
    };
    options.DefaultRequestCulture = new RequestCulture("en-en");
    options.SupportedCultures = culture;
    options.SupportedUICultures = culture;

    // options.RequestCultureProviders.Clear(); // remove predefined RequestCultureProviders
    // options.RequestCultureProviders.Add(new RouteValueRequestCultureProvider());
});

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{culture=en-us}/{controller=Home}/{action=Index}/{id?}");

app.Run();
