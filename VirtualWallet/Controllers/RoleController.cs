using Microsoft.AspNetCore.Mvc;
using VirtualWallet.Models;
using VirtualWallet.Repository.Interfaces;

namespace VirtualWallet.Controllers;

/*[ApiController] 
[Route("api/[Controller]")]
public class RoleController 
{ 
    private readonly IRoleRepository _roleRepository;
    
    public RoleController(IRoleRepository roleRepository)
    { 
        _roleRepository = roleRepository;
    }
    
    // GET: api/roles
    [HttpGet] 
    public async Task<IActionResult> Get() 
    { 
        var roles = await _roleRepository.GetAllRoles();
        return Ok(roles);
    }
    
    // GET: api/roles/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var role = await _roleRepository.GetRoleById(id);
        if (role == null) return NotFound();
        return Ok(role);
    }
    
    // POST: api/roles
    [HttpPost]
    public async Task<IActionResult> Post(Role role)
    {
        await _roleRepository.AddRole(role);
        return CreatedAtAction(nameof(Get), new { id = role.Id }, role);
    }
    
    // PUT: api/roles/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Role updatedRole)
    {
        var role = await _roleRepository.GetRoleById(id);
        if (role == null) return NotFound();
        role.Name = updatedRole.Name;
        role.Description = updatedRole.Description;
        await _roleRepository.UpdateRole(role);
        return NoContent();
    }

    // DELETE: api/roless/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var role = await _roleRepository.GetRoleById(id);
        if (role == null) return NotFound();
        await _roleRepository.DeleteRole(id);
        return NoContent();
    } 
}*/  
