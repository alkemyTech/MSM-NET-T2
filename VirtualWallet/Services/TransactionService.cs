using VirtualWallet.DataAccess;
using VirtualWallet.Models;
using VirtualWallet.Models.DTO;
using VirtualWallet.Services.Interfaces;

namespace VirtualWallet.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly UnitOfWork _unitOfWork;

        public TransactionService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Object> getAllTransactionsAsync(int pageNumber, int pageSize, string userId)
        {
            var transactions = await _unitOfWork.TransactionRepo.getAll();
            var filteredTransactions = transactions.Where(t => t.UserId.ToString() == userId).OrderByDescending(t => t.Date); //Filtro las transacciones realizadas por el usuario logueado

            //Implemento la paginación de la consulta
            var pagedTransactions = filteredTransactions
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            //En caso de no tener elementos se devuelve un objeto null
            if (pagedTransactions.Count() == 0)
            {
                return null;
            }

            var totalPages = (int)Math.Ceiling((double)filteredTransactions.Count() / pageSize);

            //Links para página Anterior y Siguiente

            var prevPage = pageNumber > 1 ? "Get?pageNumber=" + (pageNumber - 1) + "&pageSize=" + pageSize : null;

            var nextPage = pageNumber < (int)Math.Ceiling((double)filteredTransactions.Count() / pageSize) ? "Get?pageNumber=" + (pageNumber + 1) + "&pageSize=" + pageSize : null;

            //Formateo la salida con un objeto que contiene las transacciones efectuadas por el usuario, junto con los links para la paginación
            var result = new
            {
                Transactions = pagedTransactions,
                PrevPage = prevPage,
                NextPage = nextPage,
                TotalPages = totalPages,
            };

            return result;
        }

        public async Task<Transaction> getTransactionAsync(int transactionId, string userId)
        {
            //Obtengo la transacción consultada 
            var transaction = await _unitOfWork.TransactionRepo.getById(transactionId);

            //De no existir la transacción hecha por el usuario se devuelve un null
            if (transaction == null || !userId.Equals(transaction.AccountId.ToString()))
            {
                return null;
            }
            return transaction;
        }

        public async Task<Transaction> addTransactionAsync(TransactionDTO transactionDTO, string userId)
        {
            //Se obtiene la transacción añadida
            var transaction = new Transaction
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

            if (transaction == null)
            {
                return null;
            }
            //En caso de ser un depósito se suma la cantidad de dinero ingresada a la cuenta del usuario
            if (transaction.Type.Equals("topup"))
            {
                var account = await _unitOfWork.AccountRepo.GetById(transaction.AccountId);
                account.Money += transaction.Amount;
            }

            //En caso de ser una transferencia se suma la cantidad de dinero ingresada a la cuenta destinataria
            else
            {
                var account = await _unitOfWork.AccountRepo.GetById((int)transaction.ToAccountId);
                account.Money += transaction.Amount;
            }

            await _unitOfWork.TransactionRepo.Insert(transaction);
            await _unitOfWork.SaveChangesAsync();

            return transaction;
        }

        public async Task<Transaction> updateTransactionAsync(int transactionId, TransactionDTO transaction)
        {
            //Se obtiene la transacción añadida
            var _transaction = await _unitOfWork.TransactionRepo.getById(transactionId);

            if (_transaction == null)
            {
                return null;
            }

            //Se modifica la transacción
            _transaction.Amount = transaction.Amount;
            _transaction.Concept = transaction.Concept;
            _transaction.Date = transaction.Date;
            _transaction.Type = transaction.Type;


            if (transaction.ToAccountId != null) { _transaction.ToAccountId = transaction.ToAccountId; }

            await _unitOfWork.TransactionRepo.Update(_transaction);
            await _unitOfWork.SaveChangesAsync();

            return _transaction; 
        }

        public async Task<bool> deleteTransactionAsync(int transactionId)
        {
            //Se obtiene la transacción consultada
            var transaction = await _unitOfWork.TransactionRepo.getById(transactionId);

            //De no existir la transaccion se devuelve False
            if (transaction == null)
            {
                return false;
            }

            await _unitOfWork.TransactionRepo.Delete(transactionId);
            await _unitOfWork.SaveChangesAsync();

            //En caso de realizarse con exito se devuelve un booleano True
            return true;
        }
    }
}