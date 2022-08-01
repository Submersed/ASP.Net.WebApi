using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ineorwebapiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ineorwebapiContext") ?? throw new InvalidOperationException("Connection string 'ineorwebapiContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Using InMemory Caching
builder.Services.AddDbContext<ineorwebapiContext>(options => options.UseInMemoryDatabase("ineorwebapiDB"));

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
