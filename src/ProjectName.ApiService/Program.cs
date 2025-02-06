using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjectName.ServiceDefaults;
using ProjectName.ServiceDefaults.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Load Configuration
var configuration = builder.Configuration;

// Configure Database Connections
var portfolioDbConnection = configuration.GetConnectionString("PortfolioDb");
builder.AddSqlServerDbContext<ApplicationDbContext>(portfolioDbConnection);

builder.AddKeyedSqlServerClient("mainDb");
builder.AddKeyedSqlServerClient("loggingDb");

// Define XML documentation path dynamically for Swagger
string xmlFilePath = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
builder.Services.AddSwaggerDocumentation(xmlFilePath);

// Register API Controllers and OpenAPI
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Enable CORS for external access
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

// Enable Logging for EF Core Queries (Useful for Debugging)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(portfolioDbConnection)
           .EnableSensitiveDataLogging()
           .LogTo(Console.WriteLine, LogLevel.Information));

var app = builder.Build();

// Map Default Endpoints
app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate(); // Apply pending migrations
    }

    app.MapOpenApi();
    app.UseSwaggerDocumentation();
}

// Middleware
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors("AllowAll");
app.MapControllers();
app.Run();
