using Microsoft.EntityFrameworkCore;

namespace ChatService.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddDbContext<Application.Persistence.ChatDbContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
        });
        
        // Add services to the container.
        builder.Services.AddAuthorization();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        
        var app = builder.Build();
        
        app.UseHttpsRedirection();

        app.UseAuthorization();
        
        app.Run();
    }
}