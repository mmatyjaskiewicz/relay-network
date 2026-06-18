using Microsoft.EntityFrameworkCore;
using AuthService.Application.Persistence;

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
        
        var app = builder.Build();
        
        app.UseHttpsRedirection();

        app.UseAuthorization();
        
        app.Run();
    }
}