using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using VirtualWallet.Models;

namespace VirtualWallet.DataAccess;

public class VirtualWalletDbContext : DbContext
{
    public VirtualWalletDbContext(DbContextOptions<VirtualWalletDbContext> options, IConfiguration configuration) : base(options)
    {

    }

    public DbSet<Role> Roles { get; set; }

    public DbSet<User> Users { get; set; } 
    public DbSet<Account> Accounts { get; set; }

    public DbSet<Transaction> Transactions { get; set; }

    public DbSet<Catalogue> Catalogues { get; set; }
    public DbSet<FixedTermDeposit> FixedTermDeposits { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Especificar al modelo que las tablas van en singular 
        modelBuilder.Entity<Role>().ToTable("Role");
        modelBuilder.Entity<User>().ToTable("User");
        modelBuilder.Entity<Account>().ToTable("Account");
        modelBuilder.Entity<Transaction>().ToTable("Transaction");
        modelBuilder.Entity<Catalogue>().ToTable("Catalogue");
        modelBuilder.Entity<FixedTermDeposit>().ToTable("FixedTerm");

        // Seed Role
        modelBuilder.Entity<Role>().HasData(
            new Role { Id = 1, Name = "Admin", Description = "Permisos para agregar y eliminar usuarios" },
            new Role { Id = 2, Name = "Regular", Description = "Cliente" }
        );

        // Seed User
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, First_name = "Pedro", Last_name = "Gonzalez", Email = "pedro@pedro", Password = "123", Points = 50 , Role_Id=1},
            new User { Id = 2, First_name = "Fede", Last_name = "Perez", Email = "fede@fede", Password = "1234", Points = 504, Role_Id = 2 },
            new User { Id = 3, First_name = "Maca", Last_name = "Pereira", Email = "maca@maca", Password = "12345", Points = 5, Role_Id = 2 },
            new User { Id = 4, First_name = "Sofia", Last_name = "Gomez", Email = "mac@maca", Password = "123456", Points = 23, Role_Id = 2 }
        );
        // Seed Account
        modelBuilder.Entity<Account>().HasData(
            new Account { Id = 1, CreationDate = new DateTime(2018, 2, 21), Money = 210600, IsBlocked = true, UserId=1 },
            new Account { Id = 2, CreationDate = new DateTime(2018, 5, 15), Money = 100980, IsBlocked = false, UserId = 2 },
            new Account { Id = 3, CreationDate = new DateTime(2019, 8, 8), Money = 250990, IsBlocked = false, UserId = 3 },
            new Account { Id = 4, CreationDate = new DateTime(2019, 11, 5), Money = 420560, IsBlocked = false, UserId = 4 }

        );
        // Seed Transaction

        modelBuilder.Entity<Transaction>().HasData(
            new Transaction { transactionId = 1, Amount = 7500, Concept = "Compra en l�nea 3", Date = new DateTime(2023, 1, 15), Type = "payment", AccountId = 2, UserId = 2 },
            new Transaction { transactionId = 2, Amount = 3000, Concept = "Dep�sito en efectivo 2", Date = new DateTime(2023, 2, 5), Type = "topup", AccountId = 1, UserId = 1 },
            new Transaction { transactionId = 3, Amount = 9000, Concept = "Pago de factura 2", Date = new DateTime(2023, 3, 20), Type = "payment", AccountId = 3, UserId = 3, ToAccountId = 2 },
            new Transaction { transactionId = 4, Amount = 6000, Concept = "Transferencia bancaria 2", Date = new DateTime(2023, 4, 10), Type = "payment", AccountId = 2, UserId = 2, ToAccountId = 3 },
            new Transaction { transactionId = 5, Amount = 4200, Concept = "Recarga de tarjeta 2", Date = new DateTime(2023, 5, 5), Type = "topup", AccountId = 4, UserId = 4 }
            );

        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.User)
            .WithMany(u => u.Transactions)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.NoAction); // O NoAction para OnUpdate


        // Seed Catalogue

        modelBuilder.Entity<Catalogue>().HasData(
            new Catalogue { Id = 3, ProductDescription = "Auriculares inalámbricos", Image = "Image3.jpg", Points = 32000 },
            new Catalogue { Id = 4, ProductDescription = "Tarjeta de regalo de $50", Image = "Image4.jpg", Points = 5000 },
            new Catalogue { Id = 5, ProductDescription = "Camiseta de edición limitada", Image = "Image5.jpg", Points = 12000 },
            new Catalogue { Id = 6, ProductDescription = "Botella de vino premium", Image = "Image6.jpg", Points = 15000 }
           );

        // Seed FixedTermDeposit

        modelBuilder.Entity<FixedTermDeposit>().HasData(
            new FixedTermDeposit { Id = 1, AccountId = 1, UserId = 1, Amount = 10000.00M, CreationDate = new DateTime(2023, 3, 1), ClosingDate = new DateTime(2023, 6, 1), NominalRate = 5.0M, State = "Active" },
            new FixedTermDeposit { Id = 2, AccountId = 2, UserId = 2, Amount = 15000.00M, CreationDate = new DateTime(2023, 4, 15), ClosingDate = new DateTime(2023, 8, 15), NominalRate = 4.5M, State = "Active" },
            new FixedTermDeposit { Id = 3, AccountId = 3, UserId = 3, Amount = 20000.00M, CreationDate = new DateTime(2023, 6, 10), ClosingDate = new DateTime(2023, 12, 10), NominalRate = 4.0M, State = "Active" },
            new FixedTermDeposit { Id = 4, AccountId = 4, UserId = 4, Amount = 12000.00M, CreationDate = new DateTime(2023, 8, 5), ClosingDate = new DateTime(2024, 2, 5), NominalRate = 4.2M, State = "Active" }

            );
        
        modelBuilder.Entity<FixedTermDeposit>()
            .HasOne(f => f.User)
            .WithMany(u => u.FixedTermDeposits)
            .HasForeignKey(f => f.UserId)
            .OnDelete(DeleteBehavior.Restrict); // O Restrict para OnUpdate




    }

}