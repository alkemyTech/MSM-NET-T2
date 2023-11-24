using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VirtualWallet.DataAccess;
using VirtualWallet.Models;

namespace VirtualWallet.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class RoleController : ControllerBase
{
    private readonly UnitOfWork _unitOfWork;
    public RoleController(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // GET: api/roles
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Get()
    {
        var roles = await _unitOfWork.RoleRepo.GetAll();
        if (roles == null)
        {
            return NotFound();
        }
        return Ok(roles);
    }

    // GET: api/roles/{id}
    [HttpGet]
    [Route("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Get(int id)
    {
        var role = await _unitOfWork.RoleRepo.GetById(id);
        if (role == null)
        {
            return NotFound();
        }
        return Ok(role);
    }

    // POST: api/roles
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Post(Role role)
    {
        if (role == null)
        {
            return BadRequest("El rol no posee datos v�lidos");
        }

        if (role.Id == 0 ||
            role.Name.Equals("Admin") ||
            role.Name.Equals("Cliente") ||
            role.Description.Length <= 5
            )
        {
            return BadRequest("La cuenta no pudo ser creada: uno o m�s errores encontrados");
        }

        var _role = new Role
        {
            Id = role.Id,
            Name = role.Name,
            Description = role.Description
        };

        await _unitOfWork.RoleRepo.Insert(_role);
        await _unitOfWork.SaveChangesAsync();
        return CreatedAtAction("Get", new { id = role.Id }, role);
    }

    // PUT: api/roles/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Role role)
    {
        var _role = await _unitOfWork.RoleRepo.GetById(id);
        if (_role == null)
        {
            return NotFound();
        }
        _role.Name = role.Name;
        _role.Description = role.Description;
        await _unitOfWork.RoleRepo.Update(_role);
        await _unitOfWork.SaveChangesAsync();
        return Ok();
    }

    // DELETE: api/roles/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var role = await _unitOfWork.RoleRepo.GetById(id);
        if (role == null)
        {
            return NotFound();
        }
        await _unitOfWork.RoleRepo.Delete(id);
        await _unitOfWork.SaveChangesAsync();
        return Ok();
    }
}