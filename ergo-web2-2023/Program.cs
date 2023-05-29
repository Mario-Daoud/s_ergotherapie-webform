using ergo_web2_2023.Data;
using ergo_web2_2023.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using ergo_web2_2023.Repositories.Interfaces;
using ergo_web2_2023.Models.Entities;
using ergo_web2_2023.Repositories;
using ergo_web2_2023.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using ergo_web2_2023.Data.UserDB;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddDefaultUI()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();


//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddAutoMapper(typeof(Program));

//email confirmation
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration);


builder.Services.AddTransient<IBasicOperationsDAO<Form>, FormDAO>();
builder.Services.AddTransient<IBasicOperationService<Form>, FormService>();

builder.Services.AddTransient<IBasicOperationsDAO<FormGroup>, FormGroupDAO>();
builder.Services.AddTransient<IBasicOperationService<FormGroup>, FormGroupService>();

builder.Services.AddTransient<IBasicOperationsDAO<FormQuestion>, FormQuestionDAO>();
builder.Services.AddTransient<IBasicOperationService<FormQuestion>, FormQuestionService>();
builder.Services.AddTransient<IFormQuestionDAO<FormQuestion>, FormQuestionDAO>();
builder.Services.AddTransient<IFormQuestionService<FormQuestion>, FormQuestionService>();

builder.Services.AddTransient<IBasicOperationsDAO<Question>, QuestionDAO>();
builder.Services.AddTransient<IBasicOperationService<Question>, QuestionService>();
builder.Services.AddTransient<IQuestionDAO<Question>, QuestionDAO>();
builder.Services.AddTransient<IQuestionService<Question>, QuestionService>();

builder.Services.AddTransient<IBasicOperationsDAO<Subquestion>, SubquestionDAO>();
builder.Services.AddTransient<IBasicOperationService<Subquestion>, SubquestionService>();
builder.Services.AddTransient<ISubquestionDAO<Subquestion>, SubquestionDAO>();
builder.Services.AddTransient<ISubquestionService<Subquestion>, SubquestionService>();

builder.Services.AddTransient<IQuestionOptionDAO<QuestionOption>, QuestionOptionDAO>();
builder.Services.AddTransient<IQuestionOptionService<QuestionOption>, QuestionOptionService>();


builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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

/*
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
*/

app.UseEndpoints(endpoints =>
{
    endpoints.MapAreaControllerRoute(
            name: "AdministratorHomePage",
            areaName: "Administrator",
            pattern: "Administrator/{controller=Home}/{action=Index}");

    endpoints.MapAreaControllerRoute(
             name: "SuperuserHomePage",
             areaName: "Superuser",
             pattern: "Superuser/{controller=Home}/{action=Index}");

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

});
app.MapRazorPages();

app.Run();