using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VirtualWallet.Models;
using VirtualWallet.Repository.Interfaces;
using VirtualWallet.Services.Interfaces;

namespace VirtualWallet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    // GET: api/accounts
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var accounts = await _accountService.GetAllAccounts();
        return Ok(accounts);
    }

    // GET: api/accounts/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var account = await _accountService.GetAccountById(id);
        if (account == null) return NotFound();
        return Ok(account);
    }

    // POST: api/accounts
    [HttpPost]
    public async Task<IActionResult> Post(Account account)
    {
        await _accountService.AddAccount(account);
        return CreatedAtAction(nameof(Get), new { id = account.Id }, account);
    }

    // PUT: api/accounts/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Account updatedAccount)
    {
        var account = await _accountService.GetAccountById(id);
        if (account == null) return NotFound();
        account.CreationDate = updatedAccount.CreationDate;
        account.Money = updatedAccount.Money;
        account.IsBlocked = updatedAccount.IsBlocked;
        await _accountService.UpdateAccount(account);
        return NoContent();
    }
    
    // DELETE: api/accounts/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var account = await _accountService.GetAccountById(id);
        if (account == null) return NotFound();
        await _accountService.DeleteAccount(id);
        return NoContent();
    }
}