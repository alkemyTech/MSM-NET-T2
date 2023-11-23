using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VirtualWallet.Models;
using VirtualWallet.Models.DTO;
using VirtualWallet.Services;
using VirtualWallet.DataAccess;
using VirtualWallet.Services.Interfaces;

namespace VirtualWallet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FixedTermController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public FixedTermController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        //ADMIN ROLE
        [HttpGet]
        [Route("GetAll")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get()
        {
            if (!User.IsInRole("Admin"))
            {
                // Usuario no autorizado
                return Unauthorized("Usuario no autorizado");
            }
            var fixedTerms = await _unitOfWork.FixedTermRepo.GetAll();

            if (fixedTerms == null)
            {
                return NotFound("No existen plazos fijos");
            }
            return Ok(fixedTerms);
        }

        //ADMIN ROLE
        [HttpGet]
        [Route("GetById{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(int id)
        {
            if (!User.IsInRole("Admin"))
            {
                // Usuario no autorizado
                return Unauthorized("Usuario no autorizado");
            }
            var fixedTerms = await _unitOfWork.FixedTermRepo.GetById(id);

            if (fixedTerms == null)
            {
                return NotFound("Plazo fijo inexistente");
            }

            return Ok(fixedTerms);
        }

        //ADMIN ROLE
        [HttpPost]
        [Route("Post")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post(FixedTermDeposit fixedTerm)
        {
            if (!User.IsInRole("Admin"))
            {
                // Usuario no autorizado
                return Unauthorized("Usuario no autorizado");
            }
            var _fixedTerm = new FixedTermDeposit
            {
                Id = fixedTerm.Id,
                UserId = fixedTerm.UserId,
                AccountId = fixedTerm.AccountId,
                Amount = fixedTerm.Amount,
                CreationDate = fixedTerm.CreationDate,
                ClosingDate = fixedTerm.ClosingDate,
                NominalRate = fixedTerm.NominalRate,
                State = fixedTerm.State
            };

            await _unitOfWork.FixedTermRepo.Insert(_fixedTerm);
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = fixedTerm.Id }, fixedTerm);
        }

        //ADMIN ROLE
        [HttpPut]
        [Route("Edit/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditFixedTerm(int id, FixedTermDeposit fixedTerm)
        {
            if (!User.IsInRole("Admin"))
            {
                // Usuario no autorizado
                return Unauthorized("Usuario no autorizado");
            }
            var _fixedTerm = await _unitOfWork.FixedTermRepo.GetById(id);

            if (_fixedTerm == null)
            {
                return NotFound("Plazo fijo inexistente");
            }
            _fixedTerm.UserId = fixedTerm.UserId;
            _fixedTerm.AccountId = fixedTerm.AccountId;
            _fixedTerm.Amount = fixedTerm.Amount;
            _fixedTerm.CreationDate = fixedTerm.CreationDate;
            _fixedTerm.ClosingDate = fixedTerm.ClosingDate;
            _fixedTerm.NominalRate = fixedTerm.NominalRate;
            _fixedTerm.State = fixedTerm.State;

            await _unitOfWork.FixedTermRepo.Update(_fixedTerm);
            await _unitOfWork.SaveChangesAsync();

            return Ok("Plazo fijo editado con exito");
        }

        //ADMIN ROLE
        [HttpDelete]
        [Route("Delete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!User.IsInRole("Admin"))
            {
                // Usuario no autorizado
                return Unauthorized("Usuario no autorizado");
            }
            var catalogue = await _unitOfWork.FixedTermRepo.GetById(id);

            if (catalogue == null)
            {
                return NotFound("Plazo fijo inexistente");
            }

            await _unitOfWork.FixedTermRepo.Delete(id);
            await _unitOfWork.SaveChangesAsync();

            return Ok("Plazo fijo eliminado con exito");
        }



        //REGULAR ROLE
        [HttpGet]
        [Route("GetMyFixedTermById/{id}")]
        [Authorize(Roles = "Regular")]
        public async Task<IActionResult> getMyFixedTermById(int id)
        {
            var userIdValue = User.FindFirstValue("Id");
            var userId = int.Parse(userIdValue);
            var fixedTermValue = id;
            var fixedTerms = await _unitOfWork.FixedTermRepo.GetMyFixedTerms(userId);
            var myFixedTerm = fixedTerms.Where(t => t.Id == fixedTermValue);
            var _fixedTerm = await _unitOfWork.FixedTermRepo.GetById(id);

            if (myFixedTerm.Equals("") || myFixedTerm.Count() == 0 || myFixedTerm == null)
            {
                return NotFound("Este plazo fijo no corresponde a un plazo fijo creado por este usuario");
            }
            return Ok(_fixedTerm);
        }

        //REGULAR ROLE
        [HttpGet("GetMyFixedTerms")]
        [Authorize(Roles = "Regular")]
        public async Task<IActionResult> GetByUserId()
        {
            var userIdValue = User.FindFirstValue("Id");
            var userId = int.Parse(userIdValue);
            var fixedTerms = await _unitOfWork.FixedTermRepo.GetAll();
            var myFixedTerm = fixedTerms.Where(t => t.UserId == userId);


            if (myFixedTerm == null)
            {
                return NotFound("Este usuario no contiene plazos fijos");
            }

            return Ok(myFixedTerm);
        }

        //REGULAR ROLE
        [HttpPost("Insert")]
        [Authorize(Roles = "Regular")]
        public async Task<IActionResult> Insert(FixedTermDepositDTO fixedTerm)
        {
            var userIdValue = User.FindFirstValue("Id");
            var userId = int.Parse(userIdValue);
            var _fixedTerm = new FixedTermDeposit
            {
                Id = fixedTerm.Id,
                UserId = userId,
                AccountId = fixedTerm.AccountId,
                Amount = fixedTerm.Amount,
                CreationDate = fixedTerm.CreationDate,
                ClosingDate = fixedTerm.ClosingDate,
                NominalRate = fixedTerm.NominalRate,
                State = fixedTerm.State
            };

            await _unitOfWork.FixedTermRepo.Insert(_fixedTerm);
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = fixedTerm.Id }, fixedTerm);
        }

        //REGULAR ROLE
        [HttpPut]
        [Route("EditMyFixedTerm/{id}")]
        [Authorize(Roles = "Regular")]
        public async Task<IActionResult> EditMyFixedTerm(int id, FixedTermDepositDTO fixedTerm)
        {
            var userIdValue = User.FindFirstValue("Id");
            var userId = int.Parse(userIdValue);
            var fixedTermValue = id;
            var fixedTerms = await _unitOfWork.FixedTermRepo.GetMyFixedTerms(userId);
            var myFixedTerm = fixedTerms.Where(t => t.Id == fixedTermValue);
            var _fixedTerm = await _unitOfWork.FixedTermRepo.GetById(id);


            if (myFixedTerm.Equals("") || myFixedTerm.Count() == 0 || myFixedTerm == null)
            {
                return NotFound("Este plazo fijo no puede editarse dado que no pertenece a este usuario");
            }

            _fixedTerm.UserId = userId;
            _fixedTerm.AccountId = fixedTerm.AccountId;
            _fixedTerm.Amount = fixedTerm.Amount;
            _fixedTerm.CreationDate = fixedTerm.CreationDate;
            _fixedTerm.ClosingDate = fixedTerm.ClosingDate;
            _fixedTerm.NominalRate = fixedTerm.NominalRate;
            _fixedTerm.State = fixedTerm.State;

            await _unitOfWork.FixedTermRepo.Update(_fixedTerm);
            await _unitOfWork.SaveChangesAsync();
            return Ok("Plazo fijo editado con exito");
        }

        //REGULAR ROLE
        [HttpDelete]
        [Route("DeleteMyFixedTerm/{id}")]
        [Authorize(Roles = "Regular")]
        public async Task<IActionResult> DeleteMyFixedTerm(int id)
        {

            var userIdValue = User.FindFirstValue("Id");
            var userId = int.Parse(userIdValue);
            var fixedTermValue = id;
            var fixedTerms = await _unitOfWork.FixedTermRepo.GetMyFixedTerms(userId);
            var myFixedTerm = fixedTerms.Where(t => t.Id == fixedTermValue);


            if (myFixedTerm.Equals("") || myFixedTerm.Count() == 0 || myFixedTerm == null)
            {
                return NotFound("Este plazo fijo no puede eliminarse dado que no pertenece a este usuario");
            }
            await _unitOfWork.FixedTermRepo.Delete(id);
            await _unitOfWork.SaveChangesAsync();

            return Ok("Plazo fijo eliminado con exito");
        }
    }
}