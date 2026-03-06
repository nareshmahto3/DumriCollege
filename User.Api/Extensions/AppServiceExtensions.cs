using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using System.Text;

using User.Api.Services;

namespace User.Api.Extensions
{
    public static class AppServiceExtensions
    {
        public static object JwtBearerDefaults { get; private set; }

        //public static WebApplicationBuilder AddAppService(this WebApplicationBuilder builder)
        //{
        //    builder.Services.AddDbContext<UserDbContext>(options =>
        //        options.UseSqlServer(builder.Configuration.GetConnectionString("MyUserDbConnection")));

        //    builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JWT:JwtOptions"));

        //    builder.Services.AddScoped<IUserService, UserServiceRepo>();
        //    builder.Services.AddScoped<IAuthService, AuthService>();

        //    builder.Services.AddSwaggerGen(option =>
        //    {
        //        option.SwaggerDoc("v1", new OpenApiInfo
        //        {
        //            Title = "UserService.Api",
        //            Version = "v1"
        //        });
        //        option.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
        //        {
        //            Name = "Authorization",
        //            Description = "Enter Bearer Token as follows: `Bearer Generated-JWT-token`",
        //            In = ParameterLocation.Header,
        //            Scheme = JwtBearerDefaults.AuthenticationScheme,
        //            Type = SecuritySchemeType.ApiKey

        //        });
        //        option.AddSecurityRequirement(new OpenApiSecurityRequirement
        //        {
        //            {
        //                 new OpenApiSecurityScheme
        //                 {
        //                     Reference =new OpenApiReference
        //                     {
        //                         Id =JwtBearerDefaults.AuthenticationScheme,
        //                          Type= ReferenceType.SecurityScheme
        //                     }
        //                 }, new string[]{}
        //            }
        //        });
        //    });
        //    var jwtOptions = builder.Configuration.GetSection("JWT:JwtOptions").Get<JwtOptions>();

        //    var key = Encoding.UTF8.GetBytes(jwtOptions!.Secret);

        //    builder.Services.AddAuthentication(x =>
        //    {
        //        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //    }).AddJwtBearer(x =>
        //    {
        //        x.TokenValidationParameters = new TokenValidationParameters
        //        {
        //            ValidateIssuerSigningKey = true,
        //            IssuerSigningKey = new SymmetricSecurityKey(key),
        //            ValidateIssuer = true,
        //            ValidIssuer = jwtOptions!.Issuer,
        //            ValidateAudience = true,
        //            ValidateLifetime = true,
        //            ValidAudience = jwtOptions!.Audience

        //        };
        //    });

        //    builder.Services.AddAuthentication();

        //    return builder;
        //}
    }
}
