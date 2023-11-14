using VirtualWallet.Models;

namespace VirtualWallet.Services
{
    public interface IUserService
    {
        Task <IEnumerable<User>> GetAllUsersAsync();
        Task <User> GetUserAsync(int id);
        Task AddUserAsync (User user);
        Task UpdateUserAsync (User user);
        Task DeleteUserAsync(int id);
    }
}
