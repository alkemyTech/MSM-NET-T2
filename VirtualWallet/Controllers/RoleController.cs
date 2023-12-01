using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VirtualWallet.Models.DTO;
using VirtualWallet.Services;

namespace VirtualWallet.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class RoleController : ControllerBase
{
    private readonly RoleService _roleService;
    
    public RoleController(RoleService roleService)
    {
        _roleService = roleService;
    }

    // GET: api/roles
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Get(int pageNumber=1, int pageSize= 10)
    {
        try
        { 
            var userId = User.FindFirstValue("Id"); 
            var result = await _roleService.GetAll(pageNumber, pageSize, userId);
            
            if (result == null)
            {
                throw new Exception("NOT_FOUND");
            }
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(404, new
            {
                status = 404,
                errors = new[] { new { error = ex.Message } }
            });
        }
    }

    // GET: api/roles/{id}
    [HttpGet]
    [Route("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var userId = User.FindFirstValue("Id");
            var result = await _roleService.GetById(id, userId);
            
            if (result == null)
            {
                throw new Exception("NOT_FOUND");
            }
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(404, new
            {
                status = 404,
                errors = new[] { new { error = ex.Message } }
            });
        }
    }

    // POST: api/roles
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Post(RoleDTO roleDto)
    {
        try
        {
            var userId = User.FindFirstValue("Id");
            var result = await _roleService.Insert(roleDto, userId);
            
            if (result == null)
            {
                throw new Exception("BAD_REQUEST");
            }
            
            return CreatedAtAction("Get", new { id = roleDto. Id }, result);
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

    // PUT: api/roles/{id}
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, RoleDTO role)
    {
        try
        {
           var result = await _roleService.Update(id, role);
            
           if (result == null)
           {
               throw new Exception("BAD_REQUEST");
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

    // DELETE: api/roles/{id}
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var result = await _roleService.Delete(id);

            if (!result)
            {
                throw new Exception("NOT_FOUND");
            }

            return Ok();
        }
        catch(Exception ex)
        {
            return StatusCode(404, new
            {
                status = 404,
                errors = new[] { new { error = ex.Message } }
            });
        }
    }
}