using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VirtualWallet.Models;
using VirtualWallet.Models.DTO;
using VirtualWallet.Services;

namespace VirtualWallet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly TransactionService _transactionService;

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
        public async Task<IActionResult> Post(TransactionDTO transactionDTO)
        {
            var _transactionDTO = new Transaction
            {
                transactionId = transactionDTO.transactionId,
                Amount = transactionDTO.Amount,
                Concept = transactionDTO.Concept,
                Date = transactionDTO.Date,
                Type = transactionDTO.Type,
                AccountId = transactionDTO.AccountId,
                UserId = transactionDTO.UserId,
                ToAccountId = transactionDTO.ToAccountId
            };

            await _transactionService.addTransactionAsync(_transactionDTO);

            return CreatedAtAction("Get", new { id = _transactionDTO.transactionId }, _transactionDTO);
        }

        [HttpPut]
        [Route("{transaction_id}")]
        public async Task<IActionResult> Put(int transaction_id, TransactionDTO transaction)
        {
            var _transaction = await _transactionService.getTransactionAsync(transaction_id);


            if (_transaction == null)
            {
                return NotFound();
            }

            if (_transaction.ToAccountId == null)
            {
                _transaction.Amount = transaction.Amount;
                _transaction.Concept = transaction.Concept;
                _transaction.Date = transaction.Date;
                _transaction.Type = transaction.Type;
                _transaction.AccountId = transaction.AccountId;
                _transaction.UserId = transaction.UserId;
            }
            else
            {
                _transaction.Amount = transaction.Amount;
                _transaction.Concept = transaction.Concept;
                _transaction.Date = transaction.Date;
                _transaction.Type = transaction.Type;
                _transaction.AccountId = transaction.AccountId;
                _transaction.UserId = transaction.UserId;
                _transaction.ToAccountId = transaction.ToAccountId;
            }


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
