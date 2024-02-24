using KyrsachAPI;
using KyrsachAPI.Context;
using KyrsachAPI.Service;
using Microsoft.EntityFrameworkCore;

/*var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOptions();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();

// Add Context
builder.Services.AddDbContext<TaskTrackContext>(
                options => options.UseSqlServer("TaskTrack"));

// Add services
builder.Services.AddScoped<IUserService, UserService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
app.UseAuthentication();
app.UseCors(x => x
.AllowAnyHeader()
.AllowAnyMethod()
.AllowAnyOrigin());

app.Run();*/


var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var service = builder.Services;
var startup = new Startup(service, configuration);
var webApplication = builder.Build();
webApplication.UseAuthentication();
webApplication.UseAuthorization();
startup.ConfigureWebApp(webApplication);
startup.Run();
