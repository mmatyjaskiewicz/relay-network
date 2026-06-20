using Microsoft.EntityFrameworkCore;
using AuthService.Application.Persistence;
using AuthService.Application.Validators;
using FluentValidation;

namespace AuthService.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddAuthorization();
        
        builder.Services.AddDbContext<AuthDbContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
        });
        
        builder.Services.AddValidatorsFromAssemblyContaining<RegisterRequestValidator>();
        
        var app = builder.Build();
        
        app.UseHttpsRedirection();

        app.UseAuthorization();
        
        app.Run();
    }
}