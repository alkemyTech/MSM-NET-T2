using Microsoft.AspNetCore.Mvc;
using VirtualWallet.Models;
using VirtualWallet.Models.DTO;
using VirtualWallet.Repository.Interfaces;

namespace VirtualWallet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountRepository _accountRepository;

    public AccountController(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    // GET: api/accounts
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var accounts = await _accountRepository.GetAllAccounts();
        return Ok(accounts);
    }

    // GET: api/accounts/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var account = await _accountRepository.GetAccountById(id);
        if (account == null) return NotFound();
        return Ok(account);
    }

    // POST: api/accounts
    [HttpPost]
    public async Task<IActionResult> Post(AccountDTO account)
    {
        var _account = new Account
        {
            CreationDate = DateTime.Now,
            Money = account.Money,
            IsBlocked = account.IsBlocked,
            UserId = account.UserId
        };
        await _accountRepository.AddAccount(_account);
        return CreatedAtAction(nameof(Get), new { id = account.Id }, account);
    }

    // PUT: api/accounts/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, AccountDTO updatedAccount)
    {
        var account = await _accountRepository.GetAccountById(id);
        if (account == null) return NotFound();
        account.CreationDate = updatedAccount.CreationDate;
        account.Money = updatedAccount.Money;
        account.IsBlocked = updatedAccount.IsBlocked;
        await _accountRepository.UpdateAccount(account);
        return NoContent();
    }

    // DELETE: api/accounts/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var account = await _accountRepository.GetAccountById(id);
        if (account == null) return NotFound();
        await _accountRepository.DeleteAccount(id);
        return NoContent();
    }
}