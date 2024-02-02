using KyrsachAPI.Context;
using KyrsachAPI.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace KyrsachAPI
{
    public class Startup
    {
        private IConfiguration ConfigRoot { get; }
        private IServiceCollection Servies { get; }
        private WebApplication WebApplication { get; set; }

        public Startup(IServiceCollection collection, IConfiguration config)
        {
            Servies = collection;
            ConfigRoot = config;
            ConfigureProperties();
            ConfigureContext();
            ConfigureServices();
        }

        private void ConfigureProperties()
        {
            Servies.AddOptions();
            Servies.AddControllers();
            Servies.AddEndpointsApiExplorer();
            Servies.AddSwaggerGen();
            Servies.AddHttpContextAccessor();

        }

        private void ConfigureContext()
        {
            Servies.AddDbContext<TaskTrackContext>(
                options => options.UseSqlServer("TaskTrack") );
        }

        private void ConfigureServices()
        {
            Servies.AddScoped<IUserService,UserService>();
        }

        public void ConfigureWebApp(WebApplication webApp)
        {
            WebApplication = webApp;
            // Configure the HTTP request pipeline.
            if (WebApplication.Environment.IsDevelopment())
            {
                WebApplication.UseSwagger();
                WebApplication.UseSwaggerUI();
            }
            WebApplication.UseAuthentication();
            WebApplication.UseAuthorization(); 
            WebApplication.UseHttpsRedirection();
            WebApplication.MapControllers();
            WebApplication.UseCors(x => x
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin());
        }

        public void Run()
        {
            WebApplication.Run();
        }
    }
}
