using VirtualWallet.DataAccess;
using VirtualWallet.Models;
using VirtualWallet.Models.DTO;
using VirtualWallet.Services.Interfaces;

namespace VirtualWallet.Services;

public class RoleService : IRoleService
{
    private readonly UnitOfWork _unitOfWork;

    public RoleService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Object> GetAll(int pageNumber, int pageSize, string userId)
    {
        var roles = await _unitOfWork.RoleRepo.GetAll();
        
        var pagedAccounts = roles
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();
        
        if (pagedAccounts.Count() == 0)
        {
            return null;
        }
        
        var prevPage = pageNumber > 1 ? "Get?pageNumber=" + (pageNumber - 1) + "&pageSize=" + pageSize : null;
            
        var nextPage = pageNumber < (int)Math.Ceiling((double)pagedAccounts.Count() / pageSize) ? "Get?pageNumber=" + (pageNumber + 1) + "&pageSize=" + pageSize : null;

        var result = new
        {
            Roles = pagedAccounts,
            prevPage = prevPage,
            nextPage = nextPage
        };
        
        return result;
    }
    
    public async Task<Role> GetById(int id, string  userId)
    {
        var role = await _unitOfWork.RoleRepo.GetById(id);
        
        if (role == null)
        {
            return null;
        }
        
        return role;
    }
    
    public async Task<Role> Insert(RoleDTO roleDto, string userId)
    {

        var role = new Role
        {
            Id = roleDto.Id,
            Name = roleDto.Name,
            Description = roleDto.Description
        };
        
        /*if (roleDto == null)
        {
            return BadRequest("El rol no posee datos v�lidos");
        }

        if (role.Id == 0 ||
            role.Name.Equals("Admin") ||
            role.Name.Equals("Cliente") ||
            role.Description.Length <= 5
            )
        {
            return BadRequest("La cuenta no pudo ser creada: uno o ms errores encontrados");
        }*/

        await _unitOfWork.RoleRepo.Insert(role);
        await _unitOfWork.SaveChangesAsync();

        return role;
    }

    public async Task<Role> Update(int id, RoleDTO roleDto)
    {
        var role = await _unitOfWork.RoleRepo.GetById(id);
        
        if (role == null)
        {
            return null;
        }
        
        role.Name = roleDto.Name;
        role.Description = roleDto.Description;
        
        await _unitOfWork.RoleRepo.Update(role);
        await _unitOfWork.SaveChangesAsync();
        return role;
    }
    
    public async Task<bool> Delete(int id)
    {
        var role = await _unitOfWork.RoleRepo.GetById(id);
        
        if (role == null)
        {
            return false;
        }
        
        await _unitOfWork.RoleRepo.Delete(id);
        await _unitOfWork.SaveChangesAsync();
        
        return true;
    }
}