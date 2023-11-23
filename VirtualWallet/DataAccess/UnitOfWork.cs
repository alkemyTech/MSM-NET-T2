using VirtualWallet.Repository;

namespace VirtualWallet.DataAccess;

public class UnitOfWork
{
    private readonly VirtualWalletDbContext _dbContext;

    // Entidades
    private TransactionRepository _transactionRepository;
    private FixedTermRepository _fixedTermRepository;
    private UserRepository _userRepository;
    private AccountRepository _accountRepository;
    private RoleRepository _roleRepository;
    private CatalogueRepository _catalogueRepository;

    public UnitOfWork(VirtualWalletDbContext dbContext) { _dbContext = dbContext; }

    // Transaction
    public TransactionRepository TransactionRepo
    {
        get
        {
            if (_transactionRepository == null)
            {
                return _transactionRepository = new TransactionRepository(_dbContext);
            }

            return _transactionRepository;
        }

    }
    // FixedTermDeposit
    public FixedTermRepository FixedTermRepo
    {
        get
        {
            if (_fixedTermRepository == null)
            {
                return _fixedTermRepository = new FixedTermRepository(_dbContext);
            }
            return _fixedTermRepository;
        }
    }
    // User
    public UserRepository UserRepo
    {
        get
        {
            if (_userRepository == null)
            {
                return _userRepository = new UserRepository(_dbContext);
            }
            return _userRepository;
        }
    }
    // Account
    public AccountRepository AccountRepo
    {
        get
        {
            if (_accountRepository == null)
            {
                return _accountRepository = new AccountRepository(_dbContext);
            }
            return _accountRepository;
        }
    }
    // Role
    public RoleRepository RoleRepo
    {
        get
        {
            if (_roleRepository == null)
            {
                return _roleRepository = new RoleRepository(_dbContext);
            }

            return _roleRepository;
        }
    }
    // Catalogue
    public CatalogueRepository CatalogueRepo
    {
        get
        {
            if (_catalogueRepository == null)
            {
                return _catalogueRepository = new CatalogueRepository(_dbContext);
            }

            return _catalogueRepository;
        }
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}