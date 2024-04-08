using KyrsachAPI;
using KyrsachAPI.Context;
using KyrsachAPI.Service;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var service = builder.Services;
var startup = new Startup(service, configuration);
var webApplication = builder.Build();
webApplication.UseAuthentication();
webApplication.UseAuthorization();
startup.ConfigureWebApp(webApplication);
startup.Run();
