using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DiscussionForm.Data;
using DiscussionForm.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DiscussionFormContext") ?? throw new InvalidOperationException("Connection string 'DiscussionFormContext' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString, sqlServerOptions =>
        sqlServerOptions.EnableRetryOnFailure()));

// Use ApplicationUser instead of IdentityUser
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

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
app.UseAuthentication(); // Ensure authentication middleware is added
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Added Razor Pages to app
app.MapRazorPages().WithStaticAssets();

app.Run();
