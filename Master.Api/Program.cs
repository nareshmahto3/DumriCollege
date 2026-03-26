using LibraryService.Utility.Data.Core.Interfaces;
using LibraryService.Utility.Data.Core.Repositories;
using Master.Api.CQRS.Users.DeleteUser;
using Master.Api.CQRS.Users.UpdateUser;
using Master.Api.DbConnection;
using Master.Api.DbEntities;
using Master.Api.Infrastructures;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DumriCollegeDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AdmissionDbConn")));
// Register your repository 
builder.Services.AddScoped<IRepository<CourseMaster>, CourseRepository>();
builder.Services.AddScoped<IRepository<ClassMaster>, ClassRepository>();
builder.Services.AddScoped<IRepository<GradeMaster>, GradeRepository>();

builder.Services.AddScoped<IRepository<UserMaster>, UserRepository>();
builder.Services.AddScoped<IRepository<RoleMaster>, RoleRepository>();
builder.Services.AddScoped<IRepository<UserRoleMapping>, UserRoleMappingRepository>();

builder.Services.AddDbContext<DumriCollegeDbContext>();
builder.Services.AddScoped<UpdateUserCommandHandler>();
builder.Services.AddScoped<DeleteUserCommandHandler>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork<DumriCollegeDbContext>>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
