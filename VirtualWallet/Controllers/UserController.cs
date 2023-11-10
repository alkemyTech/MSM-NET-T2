using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VirtualWallet.Models;
using VirtualWallet.Models.DTO;
using VirtualWallet.Services;

namespace VirtualWallet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;

        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _userService.GetAllUsersAsync();

            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);

        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetUserAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Post(UserDTO user)
        {
            var _user = new User
            {
                First_name = user.First_name,
                Last_name = user.Last_name,
                Email = user.Email,
                Password = user.Password,
                Points = user.Points,
                Role_Id = user.Role_Id
            }; 

            await _userService.AddUserAsync(_user);

            return CreatedAtAction("Get", new { id = user.Id }, user);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put(int user_id, UserDTO user)
        {
            var _user = await _userService.GetUserAsync(user_id);

            if (_user == null)
            {
                return NotFound();
            }

            _user.First_name = user.First_name;
            _user.Last_name = user.Last_name;
            _user.Email = user.Email;
            _user.Password = user.Password;
            _user.Points = user.Points;
            _user.Role_Id = user.Role_Id;

            await _userService.UpdateUserAsync(_user);

            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userService.GetUserAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            await _userService.DeleteUserAsync(id);

            return Ok();
        }
    }
}
