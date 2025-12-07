using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmartQuoteBuilder.Data;
using SmartQuoteBuilder.Repositories;
using SmartQuoteBuilder.Repositories.Interfaces;
using SmartQuoteBuilder.Services;
using SmartQuoteBuilder.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Product Repository
builder.Services.AddScoped<IProductRepository, ProductRepository>();
// Product Option Repository
builder.Services.AddScoped<IProductOptionRepository, ProductOptionRepository>();
// Price Calculator Service
builder.Services.AddScoped<IPriceCalculatorService, PriceCalculatorService>();
// Quote Repository
builder.Services.AddScoped<IQuoteRepository, QuoteRepository>();
// Quote Builder Service
builder.Services.AddScoped<IQuoteBuilderService, QuoteBuilderService>();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

// Initialize Swagger - for API Documentation
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    // Use swagger
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

app.Run();
