using Microsoft.EntityFrameworkCore;
using VirtualWallet.DataAccess;
using VirtualWallet.Models;

namespace VirtualWallet.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly VirtualWalletDbContext _dbContext;

        public UserRepository(VirtualWalletDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<User> GetById(int id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(c => c.Id == id);

        }

        public async Task Insert(User user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(User user)
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var _user = _dbContext.Users.FirstOrDefault(c => c.Id == id);

            if (_user != null)
            {
                _dbContext.Users.Remove(_user);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
