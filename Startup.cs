using KyrsachAPI.Context;
using KyrsachAPI.Entities;
using KyrsachAPI.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.Text;

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
            var appSettings = config.GetSection("AppSettings").Get<AppSettings>();
            string key = appSettings.SecretJWTKey;

            ConfigureProperties();
            ConfigureContext();
            ConfigureServices();
            ConfigureAuth(key);
        }

        private void ConfigureProperties()
        {
            Servies.Configure<AppSettings>(ConfigRoot.GetSection("AppSettings"));

            Servies.AddOptions();
            Servies.AddControllers();
            Servies.AddEndpointsApiExplorer();
            Servies.AddSwaggerGen();
            Servies.AddHttpContextAccessor();
            Servies.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                    builder =>
                    {
                        builder.AllowAnyHeader()
                               .AllowAnyMethod()
                               .AllowCredentials(); // Разрешить отправку куки
                    });
            });

        }

        private void ConfigureContext()
        {
            Servies.AddDbContext<TaskTrackContext>(
                options => options.UseSqlServer("TaskTrack") );
        }

        private void ConfigureServices()
        {
            Servies.AddScoped<IUserService,UserService>();
            Servies.AddScoped<ITokenService,TokenService>();
            Servies.AddScoped<IIdentityService,IdentityService>();
            Servies.AddScoped<ITasksService, TasksService>();
            Servies.AddScoped<IStatisticService, StatisticService>();
        }

        private void ConfigureAuth(string key)
        {
            Servies.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // указывает, будет ли валидироваться издатель при валидации токена
                        ValidateIssuer = true,
                        // строка, представляющая издателя
                        ValidIssuer = "TastTreakServer",
                        // будет ли валидироваться потребитель токена
                        ValidateAudience = true,
                        // установка потребителя токена
                        ValidAudience = "TastTreakServerUser",
                        // будет ли валидироваться время существования
                        ValidateLifetime = true,
                        // установка ключа безопасности
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                        // валидация ключа безопасности
                        ValidateIssuerSigningKey = true,
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies["token"];
                            return Task.CompletedTask;
                        }
                    };
                });
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
