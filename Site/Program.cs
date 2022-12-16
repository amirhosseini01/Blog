using AspNetCore.ReCaptcha;
using Identity_Sample.Areas.Identity.Helper;
using Microsoft.EntityFrameworkCore;
using Site.Data;
using Site.Features.Blog;
using Site.Features.BlogCategory;
using Site.Features.Menu;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

builder.Services.AddDbContext<DataBaseContext>(options =>
    options.UseSqlServer(connectionString));

// Indentity Context
builder.Services.RegisterContextConfigs(connectionString);

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");

// google recaptcha
builder.Services.AddReCaptcha(builder.Configuration.GetSection("ReCaptcha"));

//DI Custom Services
builder.Services.AddScoped<IMenuRep, MenuRep>();
builder.Services.AddScoped<IBlogRep, BlogRep>();
builder.Services.AddScoped<ICategoryRep, CategoryRep>();

// Add services to the container.
builder.Services.AddRazorPages().AddRazorRuntimeCompilation().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

// Identity Configuration
builder.Services.RegisterConfiguration();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
