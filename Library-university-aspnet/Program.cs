using Library_university_aspnet.Components;
using Library_university_aspnet.Components.Account;
using Library_university_aspnet.Data;
using Library_university_aspnet.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IReservationService, ReservationService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
})
.AddIdentityCookies();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

// Seed the database with roles and users
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    // Create roles if they don't exist
    string[] roleNames = { "Admin", "StandardUser" };
    IdentityResult roleResult;

    foreach (var roleName in roleNames)
    {
        var roleExist = await roleManager.RoleExistsAsync(roleName);

        if (!roleExist)
        {
            // Create the roles and seed them to the database
            roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }

    // Create admin user if not exists
    var adminUser = await userManager.FindByEmailAsync("admin@example.com");

    if (adminUser == null)
    {
        var newAdminUser = new ApplicationUser
        {
            UserName = "admin@example.com",
            Email = "admin@example.com",
            EmailConfirmed = true // Set EmailConfirmed to true
            // Add any additional properties for the admin user here
        };

        var createAdminUserResult = await userManager.CreateAsync(newAdminUser, "Admin@123");

        if (createAdminUserResult.Succeeded)
        {
            // Assign the admin user to the Admin role
            await userManager.AddToRoleAsync(newAdminUser, "Admin");
        }
    }

    // Create standard user if not exists
    var standardUser = await userManager.FindByEmailAsync("user@example.com");

    if (standardUser == null)
    {
        var newStandardUser = new ApplicationUser
        {
            UserName = "user@example.com",
            Email = "user@example.com",
            EmailConfirmed = true // Set EmailConfirmed to true
            // Add any additional properties for the standard user here
        };

        var createStandardUserResult = await userManager.CreateAsync(newStandardUser, "User@123");

        if (createStandardUserResult.Succeeded)
        {
            // Assign the standard user to the StandardUser role
            await userManager.AddToRoleAsync(newStandardUser, "StandardUser");
        }
    }

    if (adminUser != null && !adminUser.EmailConfirmed)
    {
        adminUser.EmailConfirmed = true; // Set EmailConfirmed to true
        await userManager.UpdateAsync(adminUser);
    }

    if (standardUser != null && !standardUser.EmailConfirmed)
    {
        standardUser.EmailConfirmed = true; // Set EmailConfirmed to true
        await userManager.UpdateAsync(standardUser);
    }
}

app.Run();
