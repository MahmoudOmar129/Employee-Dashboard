using Demo.BL.Interfaces;
using Demo.BL.Mapper;
using Demo.BL.Repository;
using Demo.DAL.Database;
using Demo.DAL.Extend;
using Demo.PL.Language;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
                .AddNewtonsoftJson(opt =>
                {
                    opt.SerializerSettings.ContractResolver = new DefaultContractResolver();
                })
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                        factory.Create(typeof(SharedResource));
                });



#region Connection String Service

// Enhancement ConnectionString
var connectionString = builder.Configuration.GetConnectionString("ApplicationConnection");

builder.Services.AddDbContextPool<ApplicationContext>(options =>
options.UseSqlServer(connectionString));

#endregion


#region Auto Mapper

builder.Services.AddAutoMapper(x => x.AddProfile(new DomainProfile()));

#endregion


#region Services


// Transient
//builder.Services.AddTransient<IDepartmentRep, DepartmentRep>();

// Scoped
builder.Services.AddScoped<IDepartmentRep, DepartmentRep>();
builder.Services.AddScoped<IEmployeeRep, EmployeeRep>();
builder.Services.AddScoped<ICountry, CountryRep>();
builder.Services.AddScoped<ICity, CityRep>();
builder.Services.AddScoped<IDistrict, DistrictRep>();

builder.Services.AddScoped(typeof(IGenericRep<>), typeof(GenericRep<>));


// SingleTone
//builder.Services.AddSingleton<IDepartmentRep, DepartmentRep>();


#endregion


#region Identity Configuration


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
    options =>
    {
        options.LoginPath = new PathString("/Account/Login");
        options.AccessDeniedPath = new PathString("/Account/Login");
    });

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);


builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{

    // User Name Settings
    options.User.RequireUniqueEmail = true;

    // Default Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 0;
}).AddEntityFrameworkStores<ApplicationContext>();


#endregion




var app = builder.Build();


var supportedCultures = new[]
{
   new CultureInfo("ar-EG"),
   new CultureInfo("en-US")
};

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en-US"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures,
    RequestCultureProviders = new List<IRequestCultureProvider>
    {
       new QueryStringRequestCultureProvider(),
       new CookieRequestCultureProvider()
    }
});

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
