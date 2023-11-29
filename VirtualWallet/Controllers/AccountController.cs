using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VirtualWallet.Models;
using VirtualWallet.Models.DTO;
using VirtualWallet.Services;

namespace VirtualWallet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly AccountService _accountService;
    
    public AccountController(AccountService accountService)
    {
        _accountService = accountService;
    }
    
    // GET: api/accounts
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Get(int pageNumber=1, int pageSize= 10)
    {
        try
        { 
            var userId = User.FindFirstValue("Id"); 
            var result = await _accountService.GetAll(pageNumber, pageSize, userId);
            
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

    // GET: api/accounts/{id}
    [HttpGet]
    [Route("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var userId = User.FindFirstValue("Id");
            var result = await _accountService.GetById(id, userId);
            
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

    // POST: api/accounts
    [HttpPost]
    [Authorize(Roles = "Regular")]
    public async Task<IActionResult> Post(AccountDTO accountDto)
    {
        try
        {
            var userId = User.FindFirstValue("Id");
            var result = await _accountService.Insert(accountDto, userId);
            
            if (result == null)
            {
                throw new Exception("BAD_REQUEST");
            }
            
            return CreatedAtAction("Get", new { id = accountDto.Id }, result);
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
    
    // POST api/accounts/Deposit/{id}
    [HttpPost]
    [Route("Deposit/{id}")]
    [Authorize(Roles = "Admin, Regular")]
    public async Task<IActionResult> Deposit(Transaction transaction)
    {
       try
       {
           var userId = User.FindFirstValue("Id");
           var result = await _accountService.Deposit(transaction, userId);
            
           if (result == null)
           {
               throw new Exception("BAD_REQUEST");
           }
            
           return CreatedAtAction("Get", new { transaction.transactionId, transaction.Amount }, result);
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
    
    // POST api/accounts/Transfer/{id}
    [HttpPost]
    [Route("Transfer/{id}")]
    [Authorize(Roles = "Admin, Regular")]
    public async Task<IActionResult> Transfer(Transaction transaction)
    {
        try
        {
           var userId = User.FindFirstValue("Id");
           var result = await _accountService.Transfer(transaction, userId);
            
           if (result == null)
           {
               throw new Exception("BAD_REQUEST");
           }
            
           return CreatedAtAction("Get", new { transaction.transactionId, transaction.Amount }, result); 
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
  
    // PUT: api/accounts/{id}
    [HttpPut]
    [Route("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, AccountDTO account)
    {
        try
        {
           var result = await _accountService.Update(id, account);
            
           if (result == null)
           {
               throw new Exception("BAD_REQUEST");
           }
            
           return Ok(); 
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

    // DELETE: api/accounts/{id}
    [HttpDelete]
    [Route("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var result = await _accountService.Delete(id);

            if (!result)
            {
                throw new Exception("NOT_FOUND");
            }

            return Ok();
        }
        catch(Exception ex)
        {
            return StatusCode(404, new
            {
                status = 404,
                errors = new[] { new { error = ex.Message } }
            });
        }
    }
}