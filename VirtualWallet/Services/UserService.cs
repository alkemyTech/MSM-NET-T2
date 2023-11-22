using Microsoft.EntityFrameworkCore;
using VirtualWallet.DataAccess;
using VirtualWallet.Models;
using VirtualWallet.Models.DTO;

namespace VirtualWallet.Services
{
    public class UserService : IUserService
    {
        private readonly UnitOfWork _unitOfWork;
        public UserService (UnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }

        public async Task<User> AddUserAsync(UserDTO user)
        {
            var _user = new User
            {
                First_name = user.First_name,
                Last_name = user.Last_name,
                Email = user.Email,
                Password = EncryptPass.GetSHA256(user.Password),
                Points = user.Points,
                Role_Id = 2
            };
            await _unitOfWork.UserRepo.Insert(_user);
            await _unitOfWork.SaveChangesAsync();
            return _user;
        }

        public async Task<User> DeleteUserAsync(int id)
        {
            var _user = await _unitOfWork.UserRepo.GetById(id);
            if(_user == null)
        {
                return null;
            }
            await _unitOfWork.UserRepo.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return _user;
        }

        public async Task<Object> GetAllUsersAsync(int pageNumber, int pageSize)
        {
            var users = await _unitOfWork.UserRepo.GetAll();
            var pagesUsers = users
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            if (pagesUsers.Count() == 0)
            {
                return null;
            }

            var prevPage = pageNumber > 1 ? "Get?pageNumber=" + (pageNumber - 1) + "&pageSize=" + pageSize : null;

            var nextPage = pageNumber < (int)Math.Ceiling((double)users.Count() / pageSize) ? "Get?pageNumber=" + (pageNumber + 1) + "&pageSize=" + pageSize : null;

            var result = new
            {
                Transactions = pagesUsers,
                PrevPage = prevPage,
                NextPage = nextPage
            };

            return result;
        }

        public async Task <User> GetUserAsync(int id)
        {
            var _user = await _unitOfWork.UserRepo.GetById(id);
            if (_user == null)
            {
                return null;
            }
            return _user;
        }

        public async Task<User> UpdateUserAsync(int id ,UserDTO user)
        {
            var _user = await _unitOfWork.UserRepo.GetById(id);
            if(_user == null)
            {
                return null;
            }

            _user.First_name = user.First_name;
            _user.Last_name = user.Last_name;
            _user.Email = user.Email;
            _user.Password = EncryptPass.GetSHA256(user.Password);
            _user.Points = user.Points;

            await _unitOfWork.UserRepo.Update(_user);
            await _unitOfWork.SaveChangesAsync();

            return _user;
        }
        
        public async Task<bool> BlockAccount(int id)
        {
            var account = await _unitOfWork.AccountRepo.GetById(id);

            if (account == null) { return false; }

            if (account.IsBlocked) { return false; }

            account.IsBlocked = true;

            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UnblockAccount(int id)
        {
            var account = await _unitOfWork.AccountRepo.GetById(id);

            if (account == null) { return false; }

            if (!account.IsBlocked) { return false; }

            account.IsBlocked = false;

            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
