using LibraryService.Utility.Data.Core.Interfaces;
using LibraryService.Utility.Data.Core.Repositories;
using Microsoft.EntityFrameworkCore;

using User.Api;
using User.Api.DbConnection;
using User.Api.DbEntities;
using User.Api.Extensions;
using User.Api.Infrastructures;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(MasterCommand).Assembly));

// Add services to the container.
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JWT:JwtOptions"));
//builder.AddAppService();
builder.Services.AddControllers();

builder.Services.AddScoped<IDynamicRepository, DynamicRepository>();
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



builder.Services.AddDbContext<DumriCommerceCollegeContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));
// Register your repository 
//builder.Services.AddScoped<IRepository<Employee.Api.DbEntities.Employee>, EmployeeRepository>();
//builder.Services.AddScoped<IUnitOfWork, UnitOfWork<EmployeeDbContext>>();
//builder.Services.AddMediatR(cfg =>
//    cfg.RegisterServicesFromAssembly(typeof(GetAllEmployeeQueryHandler).Assembly));

//builder.Services.AddScoped<
//    IRepository<Employee.Api.DbEntities.Employee>,
//    EmployeeRepository>();

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
