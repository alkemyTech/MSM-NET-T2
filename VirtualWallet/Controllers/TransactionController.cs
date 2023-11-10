using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VirtualWallet.Models;
using VirtualWallet.Services;

namespace VirtualWallet.Controllers
{
    public class TransactionController : ControllerBase
    {
        private readonly TransactionService  _transactionService;

        public TransactionController(TransactionService transactionService)
        {
            _transactionService = transactionService;

        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var transactions = await _transactionService.getAllTransactionsAsync();

            if (transactions == null)
            {
                return NotFound();
            }

            return Ok(transactions);

        }
        [HttpGet]
        [Route("{transaction_id}")]
        public async Task<IActionResult> GetById(int transaction_id)
        {
            var transaction = await _transactionService.getTransactionAsync(transaction_id);
            if (transaction == null)
            {
                return NotFound();
            }

            return Ok(transaction);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Post(Transaction transaction)
        {
            await _transactionService.addTransactionAsync(transaction);

            return CreatedAtAction("Get", new { id = transaction.transactionId }, transaction);
        }

        [HttpPut]
        [Route("{transaction_id}")]
        public async Task<IActionResult> Put(int transaction_id, Transaction transaction)
        {
            var _transaction = await _transactionService.getTransactionAsync(transaction_id);

            if (_transaction == null)
            {
                return NotFound();
            }

            _transaction.transactionId = transaction.transactionId;
            _transaction.Amount = transaction.Amount;
            _transaction.Concept = transaction.Concept;
            _transaction.Date = transaction.Date;
            _transaction.Type = transaction.Type;
            _transaction.AccountId = transaction.AccountId;
            _transaction.UserId = transaction.UserId;
            _transaction.ToAccountId = transaction.ToAccountId;


            await _transactionService.updateTransactionAsync(_transaction);

            return Ok();
        }

        [HttpDelete]
        [Route("{transaction_id}")]
        public async Task<IActionResult> Delete(int transaction_id)
        {
            var transaction = await _transactionService.getTransactionAsync(transaction_id);

            if (transaction == null)
            {
                return NotFound();
            }

            await _transactionService.deleteTransactionAsync(transaction_id);

            return Ok();
        }

    }
}
