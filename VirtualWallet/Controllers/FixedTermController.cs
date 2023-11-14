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

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var fixedTerms = await _fixedTermService.getAllFixedTermsAsync();

            if (fixedTerms == null)
            {
                return NotFound();
            }

            return Ok(fixedTerms);

        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var fixedTerms = await _fixedTermService.getFixedTermAsync(id);

            if (fixedTerms == null)
            {
                return NotFound();
            }

            return Ok(fixedTerms);
        }

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

            await _fixedTermService.addFixedTermAsync(_fixedTerm);

            return CreatedAtAction("Get", new { id = fixedTerm.Id }, fixedTerm);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put(int id, FixedTermDepositDTO fixedTerm)
        {
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

            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var catalogue = await _fixedTermService.getFixedTermAsync(id);

            if (catalogue == null)
            {
                return NotFound();
            }

            await _fixedTermService.deleteFixedTermAsync(id);

            return Ok();
        }
    }
}
