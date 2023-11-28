using VirtualWallet.DataAccess;
using VirtualWallet.Models;
using VirtualWallet.Models.DTO;
using VirtualWallet.Repository.Interfaces;
using VirtualWallet.Services.Interfaces;

namespace VirtualWallet.Services;

public class AccountService : IAccountService
{
   private readonly UnitOfWork _unitOfWork;

    public AccountService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork; 
    }
    
    public async Task<Object> GetAll(int pageNumber, int pageSize, string userId)
    {
        var accounts = await _unitOfWork.AccountRepo.GetAll();

        var pagedAccounts = accounts
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        if (pagedAccounts.Count() == 0)
        {
            return null;
        }
        
        var prevPage = pageNumber > 1 ? "Get?pageNumber=" + (pageNumber - 1) + "&pageSize=" + pageSize : null;
            
        var nextPage = pageNumber < (int)Math.Ceiling((double)pagedAccounts.Count() / pageSize) ? "Get?pageNumber=" + (pageNumber + 1) + "&pageSize=" + pageSize : null;

        var result = new
        {
            Accounts = pagedAccounts,
            prevPage = prevPage,
            nextPage = nextPage
        };
        
        return result;
    }
    
    public async Task<Account> GetById(int id, string userId)
    {
        // Se obtiene la cuenta
        var account = await _unitOfWork.AccountRepo.GetById(id);

        if (account == null)
        {
            return null;
        }

        return account;
    }

    public async Task<Account> Insert(AccountDTO accountDto, string userId)
    {
        var account = new Account
        {
            Id = accountDto.Id,
            CreationDate = DateTime.Now,
            Money = accountDto.Money,
            IsBlocked = accountDto.IsBlocked,
            UserId = accountDto.UserId
        };
        
        // Verificar la cuenta
        if (account == null)
        {
            return null;
        }
        
        await _unitOfWork.AccountRepo.Insert(account);
        await _unitOfWork.SaveChangesAsync();

        return account;
    }

    public async Task<Account> Deposit(Transaction transaction, string userId)
    { 
        // Endpoint -> Realizar depósito
        var account = await _unitOfWork.AccountRepo.GetById(transaction.AccountId);
        var user = await _unitOfWork.UserRepo.GetById(int.Parse(userId));
        
        // Verificar la cuenta
        if (account == null)
        {
            return null;
        }
        // Comparación del UserId que llega con el UserId de la Account
        if (userId.Equals(user.Id.ToString()))
        {
            // Realizar el depósito
            var deposit = account.Money += transaction.Amount;
            
            // Registrar la transacción
            var newTransaction = new Transaction
            { 
                Amount = transaction.Amount,
                Concept = transaction.Concept, 
                Date = transaction.Date, 
                Type = transaction.Type, 
                AccountId = transaction.AccountId, 
                UserId = int.Parse(userId)
                
            };
            await _unitOfWork.TransactionRepo.Insert(newTransaction);
            
            // Calcular los puntos
            var points = (int)Math.Round((int)transaction.Amount * 0.02);
            user.Points += points;
            
          await _unitOfWork.SaveChangesAsync();
        }

        return account;
    }

    public async Task<Account> Transfer(Transaction transaction, string userId)
    {

        // Endpoint -> Realizar transferencia
        var account = await _unitOfWork.AccountRepo.GetById(transaction.AccountId);
        var transferId = await _unitOfWork.AccountRepo.GetById((int)transaction.ToAccountId);
        var user = await _unitOfWork.UserRepo.GetById(transaction.UserId);
        
        // Verificar ambas cuentas
        if (account == null || transferId == null)
        {
            return null;
        }
        // Verificar que la cuenta origen tenga saldo suficiente
        if (account.Money < transaction.Amount)
        {
            return null;
        }
        
        // Comparación del UserId que llega con el UserId de la Account
        if (userId.Equals(user.Id.ToString()))
        {
            // Realizar la transferencia
            var transfer = account.Money -= transaction.Amount;

            var receive = transferId.Money += transaction.Amount;

            await _unitOfWork.TransactionRepo.Insert(transaction);
            
            // Registrar la transacción en la cuenta destino
            var toAccountTransaction = new Transaction
            {
                Amount = transaction.Amount,
                Concept = transaction.Concept,
                Date = transaction.Date,
                Type = transaction.Type,
                AccountId = transferId.Id,
                UserId = transferId.UserId
            };
            await _unitOfWork.TransactionRepo.Insert(toAccountTransaction);
            
            // Calcular los puntos de la cuenta emisora
            var points = (int)Math.Round((int)transaction.Amount * 0.03);
            user.Points += points;

            await _unitOfWork.AccountRepo.Update(account);
            await _unitOfWork.SaveChangesAsync();
        }
        return account;
    }
    
    public async Task<Account> Update(int id, AccountDTO accountDto)
    {
        // Se obtiene la cuenta
        var account = await _unitOfWork.AccountRepo.GetById(id);

        if (account == null)
        {
            return null;
        }
        
        // Se modifica la cuenta
        account.CreationDate = accountDto.CreationDate;
        account.Money = accountDto.Money;
        account.IsBlocked = accountDto.IsBlocked;
        
        await _unitOfWork.AccountRepo.Update(account);
        await _unitOfWork.SaveChangesAsync();
        return account;
    }

    public async Task<bool> Delete(int id)
    {
        // Se obtiene la cuenta
        var account = await _unitOfWork.AccountRepo.GetById(id);
        
        if (account == null)
        {
            return false;
        }
        
        await _unitOfWork.AccountRepo.Delete(id);
        await _unitOfWork.SaveChangesAsync();
        
        // En caso de realizarse con exito se devuelve un booleano True
        return true;
    }    
}