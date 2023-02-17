using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MediatR;
using SampleBank.Web;
using SampleBank.Infrastructure.Handler;
using Microsoft.AspNetCore.Antiforgery;
using SampleBank.Domain.Exceptions;
using SampleBank.Infrastructure;
using SampleBank.Application.DbLoggerObjects;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => {
    options.SignIn.RequireConfirmedEmail = false;
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
})
    .AddEntityFrameworkStores<ApplicationDbContext>();

//Our Custom Registration Method
builder.Services.RegisterServices();


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:7203/");
                      });
});

builder.Services.AddRazorPages();
builder.Logging.AddDbLogger(options =>
{
    builder.Configuration.GetSection("Logging")
    .GetSection("Database").GetSection("Options").Bind(options);
});
builder.Services.AddAntiforgery(options =>
{
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.HttpOnly = true;
    options.HeaderName = "X-CSRF-TOKEN";
});

builder.Services.AddMediatR(typeof(CreateCustomerAccountHandler).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseMigrationsEndPoint();
//}
//else
//{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
//}
//Global Exception Handler
app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors(MyAllowSpecificOrigins);


app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();


app.Use(async (context, next) =>
{
    string path = context.Request.Path.Value;

    if (path != null && !path.ToLower().Contains("/api"))
    {
        var antiforgery = context.RequestServices.GetRequiredService<IAntiforgery>();
        var tokens = antiforgery.GetAndStoreTokens(context);
        context.Response.Cookies.Append("CSRF-TOKEN", tokens.RequestToken,
            new CookieOptions() { HttpOnly = true, Secure = true });
    }

    await next();
});

app.Run();
