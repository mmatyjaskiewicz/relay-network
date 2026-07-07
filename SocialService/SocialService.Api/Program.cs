using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SocialService.Api.Exceptions;
using SocialService.Application.Clients;
using SocialService.Application.Interfaces;
using SocialService.Application.Persistence;
using SocialService.Application.Repositories;
using SocialService.Application.Services;
using SocialService.Application.Settings;

namespace SocialService.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddControllers();
        builder.Services.AddScoped<IFriendshipRepository, FriendshipRepository>();
        builder.Services.AddScoped<FriendshipService>();
        builder.Services.AddAuthorization();
        
        builder.Services.AddHttpClient<AuthClient>((provider, client) =>
        {
            var serviceUrls = provider.GetRequiredService<IOptions<ServiceUrls>>().Value;
            client.BaseAddress = new Uri(serviceUrls.Auth);
        });
        
        // Configure PostgreSQL database context
        builder.Services.AddDbContext<SocialDbContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
        });
        
        // Configure service URLs
        builder.Services.Configure<ServiceUrls>(builder.Configuration.GetSection("ServiceUrls"));
        
        // Configure JWT settings from appsettings.json
        builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

        var jwtSettings = builder.Configuration
                              .GetSection("Jwt")
                              .Get<JwtSettings>() ?? throw new InvalidOperationException("JWT settings are missing.");
        
        // Configure JWT authentication
        builder.Services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var token = context.Request.Cookies["jwt"];

                        if (!string.IsNullOrEmpty(token))
                        {
                            context.Token = token;
                        }

                        return Task.CompletedTask;
                    }
                };

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),

                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,

                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,

                    ClockSkew = TimeSpan.Zero
                };
            });
        
        // Add global exception handling
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddProblemDetails();
        
        // Build the application
        var app = builder.Build();
        
        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();
        
        app.MapControllers();
        
        app.Run();
    }
}