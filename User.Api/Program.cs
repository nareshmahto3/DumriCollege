using LibraryService.Utility.Data.Core.Interfaces;
using LibraryService.Utility.Data.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using User.Api;
using User.Api.DbConnection;
using User.Api.DbEntities;
using User.Api.Extensions;
using User.Api.Infrastructures;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JWT:JwtOptions"));
//builder.AddAppService();
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<DumriCommerceCollegeContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AdmissionDbConn")));
// Register your repository 
builder.Services.AddScoped<IRepository<MRole>, RoleRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork<DumriCommerceCollegeContext>>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
