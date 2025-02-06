using System.Reflection;
using ProjectName.ApiService.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Define XML documentation path dynamically
string xmlFilePath = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
builder.Services.AddSwaggerDocumentation(xmlFilePath);

// Register API controllers
builder.Services.AddControllers();
builder.Services.AddOpenApi(); // Using ServiceDefaults for OpenAPI

var app = builder.Build();

app.MapDefaultEndpoints(); // Automatically maps common endpoints

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerDocumentation();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();