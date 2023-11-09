using Microsoft.EntityFrameworkCore;
using VirtualWallet.Models;

namespace VirtualWallet.DataAccess;

public class VirtualWalletDbContext : DbContext
{ 
    public VirtualWalletDbContext(DbContextOptions<VirtualWalletDbContext> options) : base(options)
    {
        
    }  
    
    public DbSet<Role> Roles { get; set; }
    //public DbSet<User> Users { get; set; } 
    public DbSet<Account> Accounts { get; set; }

    public DbSet<Transaction> Transactions { get; set; }

    public DbSet<Catalogue> Catalogues { get; set; }
    public DbSet<FixedTermDeposit> FixedTermDeposits { get; set; } 
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Especificar al modelo que las tablas van en singular 
        modelBuilder.Entity<Role>().ToTable("Role");
        //modelBuilder.Entity<User>().ToTable("Role");
        modelBuilder.Entity<Account>().ToTable("Account");
        modelBuilder.Entity<Transaction>().ToTable("Transaction");
        //modelBuilder.Entity<FixedTermDeposit>().ToTable("Account");
        
        // Seed Role
        modelBuilder.Entity<Role>().HasData(
            new Role { Id = 1, Name = "Admin", Description = "Permisos para agregar y eliminar usuarios"},
            new Role { Id = 2, Name = "Regular", Description = "Cliente"}
        );
        
        // Seed User
        
        // Seed Account
        modelBuilder.Entity<Account>().HasData(
            new Account { Id = 1, CreationDate = new DateTime(2018, 2, 21), Money = 210600, IsBlocked = true },
            new Account { Id = 2, CreationDate = new DateTime(2018, 5, 15), Money = 100980, IsBlocked = false },
            new Account { Id = 3, CreationDate = new DateTime(2019, 8, 8), Money = 250990, IsBlocked = false },
            new Account { Id = 4, CreationDate = new DateTime(2019, 11, 5), Money = 420560, IsBlocked = false },
            new Account { Id = 5, CreationDate = new DateTime(2020, 4, 10), Money = 80980, IsBlocked = false },
            new Account { Id = 6, CreationDate = new DateTime(2020, 10, 3), Money = 380250, IsBlocked = false },
            new Account { Id = 7, CreationDate = new DateTime(2021, 7, 25), Money = 120900, IsBlocked = false },
            new Account { Id = 8, CreationDate = new DateTime(2021, 12, 9), Money = 130360, IsBlocked = false },
            new Account { Id = 9, CreationDate = new DateTime(2022, 1, 30), Money = 220800, IsBlocked = false },
            new Account { Id = 10, CreationDate = new DateTime(2022, 6, 13), Money = 685510, IsBlocked = false },
            new Account { Id = 11, CreationDate = new DateTime(2023, 3, 15), Money = 550380, IsBlocked = false },
            new Account { Id = 12, CreationDate = new DateTime(2023, 11, 8), Money = 310630, IsBlocked = false }  
        );
        // Seed Transaction

        modelBuilder.Entity<Transaction>().HasData(
            new Transaction { transactionId = 1, Amount = 7500, Concept = "Compra en l�nea 3", Date = new DateTime(2023, 1, 15), Type = "payment", AccountId = 2, UserId = 2 },
            new Transaction { transactionId = 2, Amount = 3000, Concept = "Dep�sito en efectivo 2", Date = new DateTime(2023, 2, 5), Type = "topup", AccountId = 1, UserId = 1 },
            new Transaction { transactionId = 3, Amount = 9000, Concept = "Pago de factura 2", Date = new DateTime(2023, 3, 20), Type = "payment", AccountId = 3, UserId = 3, ToAccountId = 2 },
            new Transaction { transactionId = 4, Amount = 6000, Concept = "Transferencia bancaria 2", Date = new DateTime(2023, 4, 10), Type = "payment", AccountId = 2, UserId = 2, ToAccountId = 3 },
            new Transaction { transactionId = 5, Amount = 4200, Concept = "Recarga de tarjeta 2", Date = new DateTime(2023, 5, 5), Type = "topup", AccountId = 4, UserId = 4 }
            );

        // Seed FixedTermDeposit
    }
}