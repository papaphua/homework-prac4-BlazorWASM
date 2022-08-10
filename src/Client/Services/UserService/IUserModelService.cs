namespace homework_prac4.Client.Services.UserService
{
    public interface IUserModelService
    {
        List<UserModel> Users { get; set; }
        Task GetUsers();
        Task<UserModel> GetUser(int id);
        Task CreateUser(UserModel user);
        Task UpdateUser(UserModel user);
        Task DeleteUser(int id);
    }
}

