using Microsoft.EntityFrameworkCore;
using VirtualWallet.Models;
using VirtualWallet.Repository;

namespace VirtualWallet.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService (IUserRepository userRepository) {
            _userRepository = userRepository;
        }

        public async Task AddUserAsync(User user)
        {
           await _userRepository.Insert(user);
        }

        public async Task DeleteUserAsync(int id)
        {
            await _userRepository.Delete(id);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAll();
        }

        public async Task <User> GetUserAsync(int id)
        {
            return await _userRepository.GetById(id);
        }

        public async Task UpdateUserAsync(User user)
        {
            await _userRepository.Update(user);
        }
    }
}
