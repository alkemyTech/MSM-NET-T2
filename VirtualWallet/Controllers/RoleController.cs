using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VirtualWallet.Models;
using VirtualWallet.Repository.Interfaces;
using VirtualWallet.Services.Interfaces;

namespace VirtualWallet.Controllers;

[ApiController] 
[Route("api/[Controller]")]
public class RoleController : ControllerBase
{ 
    private readonly IRoleService _roleService;
    
    public RoleController(IRoleService roleService)
    { 
        _roleService = roleService;
    }
    
    // GET: api/roles
    [HttpGet] 
    public async Task<IActionResult> Get() 
    { 
        var roles = await _roleService.GetAllRoles();
        return Ok(roles);
    }
    
    // GET: api/roles/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var role = await _roleService.GetRoleById(id);
        if (role == null) return NotFound();
        return Ok(role);
    }
    
    // POST: api/roles
    [HttpPost]
    public async Task<IActionResult> Post(Role role)
    {
        await _roleService.AddRole(role);
        return CreatedAtAction(nameof(Get), new { id = role.Id }, role);
    }
    
    // PUT: api/roles/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Role updatedRole)
    {
        var role = await _roleService.GetRoleById(id);
        if (role == null) return NotFound();
        role.Name = updatedRole.Name;
        role.Description = updatedRole.Description;
        await _roleService.UpdateRole(role);
        return NoContent();
    }

    // DELETE: api/roless/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var role = await _roleService.GetRoleById(id);
        if (role == null) return NotFound();
        await _roleService.DeleteRole(id);
        return NoContent();
    } 
} 
