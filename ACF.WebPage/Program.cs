using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(config =>
{
    config.LoginPath = "/Home/Index";
    config.Cookie.HttpOnly = true;
    config.SlidingExpiration = true;
    config.Cookie.Name = CookieAuthenticationDefaults.AuthenticationScheme;
    config.ExpireTimeSpan = TimeSpan.FromHours(23);
});

builder.Services.AddSession(options =>
{
    //options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.IdleTimeout = TimeSpan.FromHours(23);
    //options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    //options.Cookie.SameSite = SameSiteMode.Strict;
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddHttpClient("API", c =>
{
    c.BaseAddress = new Uri(builder.Configuration.GetSection("UrlApi").Value);
    c.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AccessUser", policy => policy.RequireRole("1", "2"));
    options.AddPolicy("NuevoCliente", policy => policy.RequireRole("1", "2"));
    options.AddPolicy("listadoClientes", policy => policy.RequireRole("1","2"));
    options.AddPolicy("AccessProductC", policy => policy.RequireRole("1", "2"));
});

// Add services to the container.
//builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
//app.MapRazorPages();

app.Run();
