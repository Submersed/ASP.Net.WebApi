using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using si.ineor.webapi.Authorization;
using si.ineor.webapi.Entities;
using si.ineor.webapi.Helpers;
using si.ineor.webapi.Services;
using BCryptNet = BCrypt.Net.BCrypt;

using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<IneorwebapiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ineorwebapiContext") ?? throw new InvalidOperationException("Connection string 'ineorwebapiContext' not found.")));

// Add services to the container.
builder.Services.AddCors();
builder.Services.AddControllers().AddJsonOptions(x =>
{
    // serialize enums as strings in api responses (e.g. Role)
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
}); 
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddScoped<IJwtUtils, JwtUtils>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMovieService, MovieService>();
//Using InMemory Caching
//builder.Services.AddDbContext<IneorwebapiContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ineorwebapiContext")));
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
      {
        {
          new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference
              {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
              },
              Scheme = "oauth2",
              Name = "Bearer",
              In = ParameterLocation.Header,

            },
            new List<string>()
          }
        });
});
var app = builder.Build();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseMiddleware<ErrorHandlerMiddleware>();

// custom jwt auth middleware
app.UseMiddleware<JwtMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// create hardcoded test users in db on startup IMPORTANT! UNCOMMENT THIS BEFORE FIRST RUN
//{
//    var testUsers = new List<User>
//    {
//        new User { Id = Guid.NewGuid(), Name = "Admin",  Username = "admin", Email="admin@webapi.si", PasswordHash = BCryptNet.HashPassword("admin"), Role = Role.Admin },
//        new User { Id = Guid.NewGuid(), Name = "Normal",  Username = "user", Email="user@webapi.si", PasswordHash = BCryptNet.HashPassword("user"), Role = Role.User }
//    };
//    var testMovies = new List<Movie>
//    {
//        new Movie { Id = Guid.NewGuid(), Title = "thoir",  Description = "admin", ReleaseDate =  new DateTime(1995, 1, 1)},
//        new Movie { Id = Guid.NewGuid(), Title = "minioni",  Description = "user", ReleaseDate =  new DateTime(1995, 1, 1)}
//    };
//    using var scope = app.Services.CreateScope();

//    var dataContext = scope.ServiceProvider.GetRequiredService<IneorwebapiContext>();
//    dataContext.Movie.AddRange(testMovies);

//    dataContext.User.AddRange(testUsers);
//    dataContext.SaveChanges();
//}

app.Run();
