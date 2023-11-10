using Microsoft.AspNetCore.Mvc;
using VirtualWallet.Models;
using VirtualWallet.Services;

namespace VirtualWallet.Controllers
{
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
        public async Task<IActionResult> Post(FixedTermDeposit fixedTerm)
        {
            await _fixedTermService.addFixedTermAsync(fixedTerm);

            return CreatedAtAction("Get", new { id = fixedTerm.Id }, fixedTerm);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put(int id, FixedTermDeposit fixedTerm)
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
