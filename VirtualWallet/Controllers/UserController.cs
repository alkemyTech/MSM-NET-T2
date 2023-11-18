using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VirtualWallet.Models;
using VirtualWallet.Models.DTO;
using VirtualWallet.Services;

namespace VirtualWallet.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;

        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();

            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(404, new
                {
                    status = 400,
                    errors = new[] { new { error = ex.Message } }
                });
            }
        }
        
        [HttpGet]
        [Authorize(Roles = "Regular")]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var _user = await _userService.GetUserAsync(id);
                if (_user == null)
                {
                    return NotFound();
                }
                return Ok(_user);
            }
            catch (Exception ex)
            {
                return StatusCode(404, new
                {
                    status = 400,
                    errors = new[] { new { error = ex.Message } }
                });
            }
        }
        
        [HttpPost]
        [Authorize(Roles = "Regular")]
        public async Task<IActionResult> Post(UserDTO user)
        {
            try
            {
                var _user = await _userService.AddUserAsync(user);
                if (_user == null)
                {
                    return NotFound();
                }
                return CreatedAtAction("Get", new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                return StatusCode(404, new
                {
                    status = 400,
                    errors = new[] { new { error = ex.Message } }
                });
            }
        }
    
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put(int id, UserDTO user)
        {
            try
            {
                var _user = await _userService.UpdateUserAsync(id, user);
                if (_user == null)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(404, new
                {
                    status = 400,
                    errors = new[] { new { error = ex.Message } }
                });
            }
        }
        
        [HttpPatch("users/block/{id}")]
        [Authorize(Roles = "Regular")]
        public async Task<IActionResult> BlockAccount(int id)
        {
            try 
            {
                if(await _userService.BlockAccount(id))
                {
                    return Ok();
                }
                else
                {
                    throw new Exception("Cuenta inexistente o bloqueada");
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(400, new
                {
                status = 400,
                errors = new[] { new { error = ex.Message } }
                });
            }
        }

        [HttpPatch("users/unblock/{id}")]
        [Authorize(Roles = "Regular")]
        public async Task<IActionResult> UnblockAccount(int id)
        {
            try
            {
                if (await _userService.UnblockAccount(id))
                {
                    return Ok();
                }
                else
                {
                    throw new Exception("Cuenta inexistente o ya se encuentra desbloqueada");
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(400, new
                {
                    status = 400,
                    errors = new[] { new { error = ex.Message } }
                });
            }
        }
        
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var user = await _userService.DeleteUserAsync(id);

                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(404, new
                {
                    status = 400,
                    errors = new[] { new { error = ex.Message } }
                });
            }

        }
    }
}