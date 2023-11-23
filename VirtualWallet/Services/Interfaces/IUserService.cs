using VirtualWallet.Models;
using VirtualWallet.Models.DTO;

namespace VirtualWallet.Services
{
    public interface IUserService
    {
        Task<Object> GetAllUsersAsync(int pageNumber, int pageSize);
        Task<User> GetUserAsync(int id);
        Task<User> AddUserAsync(UserDTO user);
        Task<User> UpdateUserAsync(int id, UserDTO user);
        Task<User> DeleteUserAsync(int id);
    }
}
