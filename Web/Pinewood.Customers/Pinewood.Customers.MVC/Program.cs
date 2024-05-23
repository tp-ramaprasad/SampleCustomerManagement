using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Pinewood.Customers.MVC.Common;
using Pinewood.Customers.MVC.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<ICustomerServices, CustomerServices>(HttpClientExtensions.GetHttpClientOptions(builder));
builder.Services.AddHttpClient<IReferenceInfoServices, ReferenceInfoServices>(HttpClientExtensions.GetHttpClientOptions(builder));
builder.Services.AddHttpClient<IUserService, UserService>(HttpClientExtensions.GetHttpClientOptions(builder));
builder.Services.AddResponseCaching();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Logging.AddLog4Net();
//builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
//builder.Services.Configure<CookiePolicyOptions>(options =>
//{
//    options.CheckConsentNeeded = context => true; // consent required
//    options.MinimumSameSitePolicy = SameSiteMode.None;
//});

//builder.Services.AddSession(opts=>
//{
//    opts.IdleTimeout = TimeSpan.FromSeconds(60);
//    opts.Cookie.IsEssential = true;
//}
//);

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
//app.UseSession();

app.UseRouting();

app.UseResponseCaching();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Users}/{action=Login}/{id?}");

app.Run();

