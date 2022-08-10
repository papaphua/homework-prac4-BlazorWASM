using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace homework_prac4.Client.Services.UserService
{
    public class UserModelService : IUserModelService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;

        public UserModelService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }

        public List<UserModel> Users { get; set; } = new List<UserModel>();

        public async Task CreateUser(UserModel user)
        {
            var result = await _http.PostAsJsonAsync("api/user", user);
            await SetUsers(result);
        } 

        private async Task SetUsers(HttpResponseMessage result)
        { 
            var response = await result.Content.ReadFromJsonAsync<List<UserModel>>();
            Users = response;           
            _navigationManager.NavigateTo("/");
        }

        public async Task DeleteUser(int id)
        {
            var result = await _http.DeleteAsync($"api/user/{id}");
            await SetUsers(result);
        }

        public async Task<UserModel> GetUser(int id)
        {
            var result = await _http.GetFromJsonAsync<UserModel>($"api/user/{id}");
            if (result != null)
                return result;
            throw new Exception("User not found");
        }

        public async Task GetUsers()
        {
            var result = await _http.GetFromJsonAsync<List<UserModel>>("api/user");
            if(result != null)
            {
                Users = result;
            }
        }

        public async Task UpdateUser(UserModel user)
        {
            var result = await _http.PutAsJsonAsync($"api/user/{user.Id}", user);
            await SetUsers(result);
        }
    }
}
