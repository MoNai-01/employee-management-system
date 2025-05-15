using Microsoft.EntityFrameworkCore;
using EmployeeManagementSystem.Models;

var builder = WebApplication.CreateBuilder(args);

// Add MySQL DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseMySql(connectionString,
        ServerVersion.AutoDetect(connectionString),
        mySqlOptions =>
        {
            mySqlOptions.EnableRetryOnFailure(
                maxRetryCount: 10,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null);
        });
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        // Apply pending migrations
        context.Database.Migrate();
        SeedData.Initialize(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating the database.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
app.UseExceptionHandler("/Home/Error");
app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

// Seed data class
public static class SeedData
{
    public static void Initialize(AppDbContext context)
    {
        if (!context.Employees.Any())
        {
            context.Employees.AddRange(
                new Employee
                {
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@example.com",
                    PhoneNumber = "123-456-7890",
                    DateOfBirth = new DateTime(1985, 5, 15),
                    Address = "123 Main St, City, Country",
                    Department = "IT",
                    Position = "Software Engineer",
                    HireDate = new DateTime(2020, 1, 10)
                },
                new Employee
                {
                    FirstName = "Jane",
                    LastName = "Smith",
                    Email = "jane.smith@example.com",
                    PhoneNumber = "987-654-3210",
                    DateOfBirth = new DateTime(1990, 8, 25),
                    Address = "456 Elm St, City, Country",
                    Department = "HR",
                    Position = "HR Manager",
                    HireDate = new DateTime(2018, 3, 22)
                }
            );
            context.SaveChanges();
        }
    }
}