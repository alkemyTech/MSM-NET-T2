using Microsoft.AspNetCore.Mvc;
using VirtualWallet.DataAccess;
using VirtualWallet.Models;
using VirtualWallet.Models.DTO;
using VirtualWallet.Services;

namespace VirtualWallet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FixedTermController : ControllerBase
    {
        // Cambiamos el service por el UnitOfWork
        
        /*private readonly FixedTermService _fixedTermService;
        
        public FixedTermController(FixedTermService fixedTermService)
        {
            _fixedTermService = fixedTermService;

        }*/
        
        private readonly UnitOfWork _unitOfWork;
        public FixedTermController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        // GET: api/fixedTermsServices
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //var fixedTerms = await _fixedTermService.getAllFixedTermsAsync();
            var fixedTerms = await _unitOfWork.FixedTermRepo.getAll();
            
            if (fixedTerms == null)
            {
                return NotFound();
            }
            return Ok(fixedTerms);
        }
        
        // GET: api/fixedTermsServices/{id}
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            //var fixedTerms = await _fixedTermService.getFixedTermAsync(id);
            var fixedTerms = await _unitOfWork.FixedTermRepo.getById(id);

            if (fixedTerms == null)
            {
                return NotFound();
            }

            return Ok(fixedTerms);
        }
        
        // POST: api/fixedTermsServices
        [HttpPost]
        public async Task<IActionResult> Post(FixedTermDepositDTO fixedTerm)
        {
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

            //await _fixedTermService.addFixedTermAsync(_fixedTerm);
            await _unitOfWork.FixedTermRepo.Insert(_fixedTerm);
            await _unitOfWork.SaveChangesAsync();
            return CreatedAtAction("Get", new { id = fixedTerm.Id }, fixedTerm);
        }
        
        // PUT: api/fixedTermsServices/{id}
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put(int id, FixedTermDepositDTO fixedTerm)
        {
            //var _fixedTerm = await _fixedTermService.getFixedTermAsync(id);
            var _fixedTerm = await _unitOfWork.FixedTermRepo.getById(id);

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

            //await _fixedTermService.updateFixedTermAsync(_fixedTerm);
            await _unitOfWork.FixedTermRepo.Update(_fixedTerm);
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }
        
        // DELETE: api/fixedTermsServices/{id}
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            //var catalogue = await _fixedTermService.getFixedTermAsync(id);
            var fixedTerm = await _unitOfWork.FixedTermRepo.getById(id);
            
            if (fixedTerm == null)
            {
                return NotFound();
            }

            //await _fixedTermService.deleteFixedTermAsync(id);
            await _unitOfWork.FixedTermRepo.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }
    }
}
