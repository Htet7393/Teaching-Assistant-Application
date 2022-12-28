using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TAapplication.Areas.Data;
using TAapplication.Data;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddAuthentication()
                 .AddGoogle(options =>
                 {
                     IConfigurationSection googleAuthNSection =
                     builder.Configuration.GetSection("Authentication:Google");

                     options.ClientId = googleAuthNSection["ClientId"];
                     options.ClientSecret = googleAuthNSection["ClientSecret"];
                 });



/*builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Prioity",
         policy => policy.RequireUserName("u0000001@utah.edu"));
    options.AddPolicy("Prioity2",
         policy => policy.RequireUserName("admin@utah.edu"));
});*/

builder.Services.AddDefaultIdentity<TAUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();



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


using (var scope = app.Services.CreateScope())
{
    var DB = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var um = scope.ServiceProvider.GetRequiredService<UserManager<TAUser>>();
    var rm = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    
    await DB.InitializeUsers(um, rm, DB);
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
