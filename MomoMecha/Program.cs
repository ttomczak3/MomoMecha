using Microsoft.EntityFrameworkCore;
using MomoMecha.Data;
using MomoMecha.Helpers;
using MomoMecha.Models;
using MomoMecha.Services;
using MomoMecha.Services.BacklogService;
using MomoMecha.Services.GundamService;
using MomoMecha.Services.SaleService;
using MomoMecha.Services.WishlistService;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();

services.AddAuthentication().AddGoogle(googleOptions =>
{
    googleOptions.ClientId = "";
    googleOptions.ClientSecret = "";
});

builder.Services.AddScoped<PhotoService>();
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));

services.AddScoped<IGundam, GundamService>();
services.AddScoped<IBacklog, BacklogService>();
services.AddScoped<IWishlist, WishlistService>();
services.AddScoped<ISale, SaleService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    //app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
