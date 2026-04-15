using Login.Api.DbConnection;
using Login.Api.Infrastructure;

using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// =====================
// ✅ Add services
// =====================

// Controllers
builder.Services.AddControllers();

// OpenAPI (if using .NET 8 minimal OpenAPI)
builder.Services.AddOpenApi();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// =====================
// ✅ DbContext
// =====================
builder.Services.AddDbContext<DumriCollegeDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("AdmissionDbConn")
    ));

// =====================
// ✅ MediatR (ONLY ONCE)
// =====================
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly())
);

// =====================
// ✅ Repositories
// =====================
builder.Services.AddScoped<UserRepository>();       // ⭐ REQUIRED
builder.Services.AddScoped<StudentRepository>();

// Optional UnitOfWork
//builder.Services.AddScoped<IUnitOfWork, UnitOfWork<DumriCollegeDbContext>>();

// =====================
// ✅ CORS (React)
// =====================
builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactPolicy", policy =>
    {
        policy.WithOrigins(
                "http://localhost:3001",
                "https://localhost:3001"
              )
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// =====================
// ✅ Build app
// =====================
var app = builder.Build();

// =====================
// ✅ Middleware
// =====================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("ReactPolicy");

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => Results.Redirect("/swagger"));

app.Run();