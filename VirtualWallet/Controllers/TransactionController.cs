using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        [Authorize(Roles = "Admin,Regular")]
        public async Task<IActionResult> Get(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var userId = User.FindFirstValue("Id");
                var result = await _transactionService.getAllTransactionsAsync(pageNumber, pageSize, userId);

                if (result == null)
                {
                    throw new Exception("NOT_FOUND");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(404, new
                {
                    status = 404,
                    errors = new[] { new { error = ex.Message } }
                });
            }
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "Admin,Regular")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var userId = User.FindFirstValue("Id");
                var result = await _transactionService.getTransactionAsync(id, userId);

                if (result == null)
                {
                    throw new Exception("NOT_FOUND");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(404, new
                {
                    status = 404,
                    errors = new[] { new { error = ex.Message } }
                });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post(TransactionDTO transactionDTO)
        {
            try
            {
                var userId = User.FindFirstValue("Id");
                var result = await _transactionService.addTransactionAsync(transactionDTO, userId);

                if (result == null)
                {
                    throw new Exception("BAD_REQUEST");
                }

                return CreatedAtAction("Get", new { id = result.transactionId }, result);
            }
            catch (Exception ex)
            {
                return StatusCode(400, new
                {
                    status = 400,
                    errors = new[] { new { error = ex.Message } }
                });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(int id, TransactionDTO transaction)
        {
            try
            {
                var result = await _transactionService.updateTransactionAsync(id, transaction);

                if (result == null)
                {
                    throw new Exception("NOT_FOUND");
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(404, new
                {
                    status = 400,
                    errors = new[] { new { error = ex.Message } }
                });
            }
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _transactionService.deleteTransactionAsync(id);

                if (!result)
                {
                    throw new Exception("NOT_FOUND");
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(404, new
                {
                    status = 404,
                    errors = new[] { new { error = ex.Message } }
                });
            }
        }

    }
}