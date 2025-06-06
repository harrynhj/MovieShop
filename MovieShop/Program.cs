using ApplicationCore.Contracts.Repository;
using ApplicationCore.Contracts.Services;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using MovieShop.Middleware;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.File(
        new Serilog.Formatting.Compact.CompactJsonFormatter(),
        "Logs/log-.json",
        rollingInterval: RollingInterval.Day,
        restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information
    )
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICastService, CastService>();
builder.Services.AddScoped<ICastRepository, CastRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();


builder.Services.AddDbContext<MovieShopDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MovieShopDbConnection"));
});
builder.Services.AddAuthentication("MyCookieAuth")
    .AddCookie("MyCookieAuth", options =>
    {
        options.Cookie.Name = "UserLoginCookie";
        options.LoginPath = "/User/Login";
    });
builder.Services.AddAuthorization();
builder.Services.AddScoped<AdminActionFilter>();
var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();