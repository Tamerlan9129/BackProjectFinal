using Core.Entities;
using Core.Utilities.FileService;
using DataAccess;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Web.Services.Abstract;
using Web.Services.Concrete;
using AdminAbstractService = Web.Areas.Admin.Services.Abstract;
using AdminConcreteService = Web.Areas.Admin.Services.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddSingleton<IFileService, FileService>();


var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(connectionString, x => x.MigrationsAssembly("DataAccess")));
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 0;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;

}).AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddScoped<IHomeMainSliderRepository, HomeMainSliderRepository>();
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IMedicalDepartmentRepository, MedicalDepartmentRepository>();
builder.Services.AddScoped<IAboutRepository, AboutRepository>();
builder.Services.AddScoped<IAboutPhotoRepository, AboutPhotoRepository>();
builder.Services.AddScoped<IWhyChooseUsRepository, WhyChooseUsRepository>();
builder.Services.AddScoped<IOurVisionRepository, OurVisionRepository>();
builder.Services.AddScoped<IHomeVideoRepository, HomeVideoRepository>();
builder.Services.AddScoped<ILastestNewsRepository, LastestNewsRepository>();
builder.Services.AddScoped<IStatisticRepository, StatisticRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
builder.Services.AddScoped<IPricingRepository, PricingRepository>();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddScoped<IBasketProductRepository, BasketProductRepository>();




builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IHomeService, HomeService>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IQuestionCategoryRepository, QuestionCategoryRepository>();
builder.Services.AddScoped<IFaqService, FaqService>();
builder.Services.AddScoped<IPricingService, PricingService>();
builder.Services.AddScoped<IShopService, ShopService>();
builder.Services.AddScoped<IBasketService, BasketService>();


builder.Services.AddScoped<AdminAbstractService.IAccountService, AdminConcreteService.AccountService>();
builder.Services.AddScoped<AdminAbstractService.IHomeMainSliderService, AdminConcreteService.HomeMainSliderService>();
builder.Services.AddScoped<AdminAbstractService.IOurVisionService, AdminConcreteService.OurVisionService>();
builder.Services.AddScoped<AdminAbstractService.IMedicalDepartmentService, AdminConcreteService.MedicalDepartmentService>();
builder.Services.AddScoped<AdminAbstractService.IDoctorService, AdminConcreteService.DoctorService>();
builder.Services.AddScoped<AdminAbstractService.IWhyChooseUsService, AdminConcreteService.WhyChooseUsService>();
builder.Services.AddScoped<AdminAbstractService.IAboutService, AdminConcreteService.AboutService>();
builder.Services.AddScoped<AdminAbstractService.ILastestNewsService,AdminConcreteService.LastestNewsService>();
builder.Services.AddScoped<AdminAbstractService.IHomeVideoService, AdminConcreteService.HomeVideoService>();
builder.Services.AddScoped<AdminAbstractService.IStatisticService, AdminConcreteService.StatisticService>();
builder.Services.AddScoped<AdminAbstractService.IPricingService, AdminConcreteService.PricingService>();
builder.Services.AddScoped<AdminAbstractService.IQuestionCategoryService, AdminConcreteService.QuestionCategoryService>();
builder.Services.AddScoped<AdminAbstractService.IQuestionService, AdminConcreteService.QuestionService>();
builder.Services.AddScoped<AdminAbstractService.IProductCategoryService, AdminConcreteService.ProductCategoryService>();
builder.Services.AddScoped<AdminAbstractService.IProductService, AdminConcreteService.ProductService>();

var app = builder.Build();



var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();

using (var scope = scopeFactory.CreateScope())
{
    var userManager = scope.ServiceProvider.GetService<UserManager<User>>();
    var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

    await DbInitialize.SeedAsync(userManager, roleManager);

}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
        name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=home}/{action=Index}/{id?}");

app.Run();
