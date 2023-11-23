using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VirtualWallet.Models.DTO;
using VirtualWallet.Services;

namespace VirtualWallet.Controllers
{
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
        [Authorize(Roles = "Admin, Regular")]
        public async Task<IActionResult> Get(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var users = await _userService.GetAllUsersAsync(pageNumber, pageSize);

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
        [Route("{id}")]
        [Authorize(Roles = "Admin,Regular")]
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
        [Authorize(Roles = "Admin, Regular")]
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
        [Authorize(Roles = "Admin, Regular")]
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
        public async Task<IActionResult> BlockAccount(int id)
        {
            try
            {
                if (await _userService.BlockAccount(id))
                {
                    return Ok();
                }
                else
                {
                    throw new Exception("Cuenta inexistente o bloqueada");
                }
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


        [HttpDelete]
        //[Authorize(Roles = "Admin")]
        [Route("{id}")]
        [Authorize(Roles = "Admin, Regular")]
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