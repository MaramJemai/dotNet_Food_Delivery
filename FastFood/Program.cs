using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FastFood.Data;
using FastFood.Areas.Identity.Data;
using FastFood.Services;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("FastFoodContextConnection") ?? throw new InvalidOperationException("Connection string 'FastFoodContextConnection' not found.");

builder.Services.AddDbContext<FastFoodContext>(options =>options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<FastFoodUser>(options =>options.SignIn.RequireConfirmedAccount = true)
        .AddRoles<IdentityRole>()

    .AddEntityFrameworkStores<FastFoodContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();


builder.Services.AddRazorPages();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration);


var app = builder.Build();
app.MapRazorPages();

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
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
