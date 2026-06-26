using System.Net.Http.Json;
using SocialService.Application.DTOs.External;
using SocialService.Application.Settings;

namespace SocialService.Application.Clients;

public class AuthClient(HttpClient httpClient)
{
    public async Task<bool> UserExistsAsync(string username)
    {
        var response = await httpClient.GetAsync($"api/user/{username}/exists");
        return response.IsSuccessStatusCode;
    }
    
    public async Task<UserData> GetUserDataAsync(string username)
    {
        var response = await httpClient.GetAsync($"api/user/{username}");
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to get user data for {username}. Status code: {response.StatusCode}");
        }
        
        var userData = await response.Content.ReadFromJsonAsync<UserData>();
        if (userData == null)
        {
            throw new Exception($"User data for {username} is null.");
        }
        
        return userData;
    }
}