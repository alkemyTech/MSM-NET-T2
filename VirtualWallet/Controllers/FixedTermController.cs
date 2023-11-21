using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using VirtualWallet.Models;
using VirtualWallet.Models.DTO;
using VirtualWallet.Services;

namespace VirtualWallet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FixedTermController : ControllerBase
    {
        private readonly FixedTermService _fixedTermService;

        public FixedTermController(FixedTermService fixedTermService)
        {
            _fixedTermService = fixedTermService;

        }

        //ADMIN ROLE
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> Get()
        {
            if (!User.IsInRole("Admin"))
            {
                // Usuario no autorizado
                return Unauthorized("Usuario no autorizado");
            }
            var fixedTerms = await _fixedTermService.getAllFixedTermsAsync();

            if (fixedTerms == null)
            {
                return NotFound();
            }

            return Ok(fixedTerms);

        }

        //ADMIN ROLE
        [HttpGet]
        [Route("GetById{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (!User.IsInRole("Admin"))
            {
                // Usuario no autorizado
                return Unauthorized("Usuario no autorizado");
            }
            var fixedTerms = await _fixedTermService.getFixedTermAsync(id);

            if (fixedTerms == null)
            {
                return NotFound();
            }

            return Ok(fixedTerms);
        }

        //ADMIN ROLE
        [HttpPost]
        [Route("Post")]
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

            await _fixedTermService.addFixedTermAsync(_fixedTerm);

            return CreatedAtAction("Get", new { id = fixedTerm.Id }, fixedTerm);
        }

        //ADMIN ROLE
        [HttpPut]
        [Route("Edit/{id}")]
        public async Task<IActionResult> EditFixedTerm(int id, FixedTermDeposit fixedTerm)
        {
            if (!User.IsInRole("Admin"))
            {
                // Usuario no autorizado
                return Unauthorized("Usuario no autorizado");
            }
            var _fixedTerm = await _fixedTermService.getFixedTermAsync(id);

            if (_fixedTerm == null)
            {
                return NotFound();
            }

            _fixedTerm.UserId = fixedTerm.UserId;
            _fixedTerm.AccountId = fixedTerm.AccountId;
            _fixedTerm.Amount = fixedTerm.Amount;
            _fixedTerm.CreationDate = fixedTerm.CreationDate;
            _fixedTerm.ClosingDate = fixedTerm.ClosingDate;
            _fixedTerm.NominalRate = fixedTerm.NominalRate;
            _fixedTerm.State = fixedTerm.State;

            await _fixedTermService.updateFixedTermAsync(_fixedTerm);

            return Ok("Plazo fijo editado con exito");
        }

        //ADMIN ROLE
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!User.IsInRole("Admin"))
            {
                // Usuario no autorizado
                return Unauthorized("Usuario no autorizado");
            }
            var catalogue = await _fixedTermService.getFixedTermAsync(id);

            if (catalogue == null)
            {
                return NotFound();
            }

            await _fixedTermService.deleteFixedTermAsync(id);

            return Ok("Plazo fijo eliminado con exito");
        }

        //REGULAR ROLE
        [HttpGet]
        [Route("GetMyFixedTermById/{id}")]
        public async Task<IActionResult> getMyFixedTermById(int id)
        {
            var userIdValue = User.FindFirstValue("Id");
            var fixedTermValue = id;
            var fixedTerms = await _fixedTermService.getAllFixedTermsByUserIdAsync(userIdValue);
            var myFixedTerm = fixedTerms.Where(t => t.Id == fixedTermValue);

            if (myFixedTerm == null)
            {
                return NotFound();
            }
            return Ok(myFixedTerm);
        }

        //REGULAR ROLE
        [HttpGet("GetMyFixedTerms")]
        public async Task<IActionResult> GetByUserId()
        {
            var userIdValue = User.FindFirstValue("Id");
            var fixedTerms = await _fixedTermService.getAllFixedTermsByUserIdAsync(userIdValue);

            if (fixedTerms == null)
            {
                throw new Exception("NOT_FOUND");
            }

            return Ok(fixedTerms);
        }

        //REGULAR ROLE
        [HttpPost("Insert")]
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

            await _fixedTermService.addFixedTermAsync(_fixedTerm);

            return CreatedAtAction("Get", new { id = fixedTerm.Id }, fixedTerm);
        }

        //REGULAR ROLE
        [HttpPut]
        [Route("EditMyFixedTerm/{id}")]
        public async Task<IActionResult> EditMyFixedTerm(int id, FixedTermDepositDTO fixedTerm)
        {
            var userIdValue = User.FindFirstValue("Id");
            var userId = int.Parse(userIdValue);
            var fixedTermValue = id;
            var fixedTerms = await _fixedTermService.getAllFixedTermsByUserIdAsync(userIdValue);
            var myFixedTerm = fixedTerms.Where(t => t.Id == fixedTermValue);
            var _fixedTerm = await _fixedTermService.getFixedTermAsync(id);


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

            await _fixedTermService.updateMyFixedTermAsync(_fixedTerm);

            return Ok("Plazo fijo editado con exito");
        }

        //REGULAR ROLE
        [HttpDelete]
        [Route("DeleteMyFixedTerm/{id}")]
        public async Task<IActionResult> DeleteMyFixedTerm(int id)
        {

            var userIdValue = User.FindFirstValue("Id");
            var fixedTermValue = id;
            var fixedTerms = await _fixedTermService.getAllFixedTermsByUserIdAsync(userIdValue);
            var myFixedTerm = fixedTerms.Where(t => t.Id == fixedTermValue);


            if (myFixedTerm.Equals("") || myFixedTerm.Count() == 0 || myFixedTerm == null)
            {
                return NotFound("Este plazo fijo no puede eliminarse dado que no pertenece a este usuario");
            }
            await _fixedTermService.deleteFixedTermAsync(id);

            return Ok("Plazo fijo eliminado con exito");
        }
    }
}
