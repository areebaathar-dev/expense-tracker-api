using ExpenseTrackerAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ---- Services ----
builder.Services.AddControllers();

// EF Core + SQL Server LocalDB (connection string lives in appsettings.json)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Swagger / OpenAPI - this is what gives you interactive, auto-generated API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Expense Tracker API",
        Version = "v1",
        Description = "A REST API for tracking personal expenses by category, built with ASP.NET Core and Entity Framework Core."
    });
});

// Allow any client (browser app, Postman, mobile app) to call this API during development
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

// ---- Middleware pipeline ----
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Expense Tracker API v1");
        options.RoutePrefix = string.Empty; // Swagger UI opens at the root URL
    });
}

app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

// Auto-apply any pending EF Core migrations on startup (convenient for a demo/portfolio project)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.Run();
