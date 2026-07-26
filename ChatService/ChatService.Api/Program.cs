using ChatService.Api.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ChatService.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddApplicationModules(builder.Configuration);
        
        var app = builder.Build();
        
        app.UseHttpsRedirection();

        app.UseAuthorization();
        
        app.Run();
    }
}