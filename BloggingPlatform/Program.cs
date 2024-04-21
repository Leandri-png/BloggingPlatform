using Microsoft.EntityFrameworkCore;
using BloggingPlatform.DataService.Models;
using BloggingPlatform.DataService;
using Auth0.AspNetCore.Authentication;
using BloggingPlatform.BusinessService;
using BloggingPlatform.DataService.Interfaces;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Configure authentication and authorization services
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies"; // Set the default authentication scheme
}).AddAuth0WebAppAuthentication(options =>
{
    options.Domain = builder.Configuration["Auth0:Domain"];
    options.ClientId = builder.Configuration["Auth0:ClientId"];
    options.Scope = "openid profile email";
});


builder.Services.AddAuthorization(); // Add this line to configure authorization services

// Add the service interface and its implementation to the service collection
builder.Services.AddScoped<IPostService, PostService>();

// Add Entity Framework Core context to the service collection
builder.Services.AddDbContext<BloggingPlatformContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BloggingPlatformContext") ?? throw new InvalidOperationException("Connection string 'BloggingPlatformContext' not found.")));

// Add controllers with views
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Migrate database on application startup
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<BloggingPlatformContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


