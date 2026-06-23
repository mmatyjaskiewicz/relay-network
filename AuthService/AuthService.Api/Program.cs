using System.Text;
using AuthService.Api.Exceptions;
using AuthService.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using AuthService.Application.Persistence;
using AuthService.Application.Repositories;
using AuthService.Application.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        // Add services to the container.
        builder.Services.AddAuthorization();
        builder.Services.AddControllers();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<Application.Services.AuthService>();
        builder.Services.AddScoped<Application.Services.TokenService>();
        
        // Configure PostgreSQL database context
        builder.Services.AddDbContext<AuthDbContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
        });
        
        // Configure JWT authentication
        builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
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

                var key = builder.Configuration["Jwt:Key"];

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(key!)
                    ),

                    ValidateIssuer = true,
                    ValidateAudience = true,

                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],

                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

        // Add FluentValidation validators
        builder.Services.AddValidatorsFromAssemblyContaining<RegisterRequestValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();
        
        // Add global exception handling
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddProblemDetails();
        
        var app = builder.Build();
        
        app.UseExceptionHandler();
        
        app.UseHttpsRedirection();

        app.UseAuthorization();
        
        app.MapControllers();
        
        app.Run();
    }
}