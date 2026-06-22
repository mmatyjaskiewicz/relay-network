using AuthService.Api.Exceptions;
using AuthService.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using AuthService.Application.Persistence;
using AuthService.Application.Repositories;
using AuthService.Application.Validators;
using FluentValidation;

namespace AuthService.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddAuthorization();
        builder.Services.AddControllers();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<Application.Services.AuthService>();
        
        builder.Services.AddDbContext<AuthDbContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
        });
        
        builder.Services.AddValidatorsFromAssemblyContaining<RegisterRequestValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();
        
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