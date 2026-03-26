using Employee.Api.CQRS.GetEmployee;
using Employee.Api.DbConnection;
using Employee.Api.Infrastructures;
using LibraryService.Utility.Data.Core.Interfaces;
using LibraryService.Utility.Data.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using User.Api;
using User.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JWT:JwtOptions"));
//builder.AddAppService();
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<EmployeeDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AdmissionDbConn")));
// Register your repository 
builder.Services.AddScoped<IRepository<Employee.Api.DbEntities.Employee>, EmployeeRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork<EmployeeDbContext>>();
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(GetAllEmployeeQueryHandler).Assembly));

builder.Services.AddScoped<
    IRepository<Employee.Api.DbEntities.Employee>,
    EmployeeRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
