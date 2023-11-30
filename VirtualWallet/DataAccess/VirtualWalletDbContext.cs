using Microsoft.EntityFrameworkCore;
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
    public DbSet<Catalogue> Catalogues { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<FixedTermDeposit> FixedTermDeposits { get; set; } 
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Especificar al modelo que las tablas van en singular 
        modelBuilder.Entity<Role>().ToTable("Role");
        modelBuilder.Entity<User>().ToTable("User");
        modelBuilder.Entity<Account>().ToTable("Account");
        modelBuilder.Entity<Catalogue>().ToTable("Catalogue");
        modelBuilder.Entity<Transaction>().ToTable("Transaction");
        modelBuilder.Entity<FixedTermDeposit>().ToTable("FixedTermDeposit");
        
        // Seed Role
        modelBuilder.Entity<Role>().HasData(
            new Role { Id = 1, Name = "Admin", Description = "Permisos para agregar y eliminar usuarios" },
            new Role { Id = 2, Name = "Regular", Description = "Cliente" }
        );
        
        // Seed User
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, First_name = "Juan", Last_name = "Diaz", Email = "juan@gmail.com", Password = EncryptPass.GetSHA256("admin"), Points = 50000, Role_Id = 1 },
            new User { Id = 2, First_name = "Abi", Last_name = "Barroso", Email = "abi@gmail.com", Password = EncryptPass.GetSHA256("admin"), Points = 50000, Role_Id = 1 },
            new User { Id = 3, First_name = "Emi", Last_name = "Brito", Email = "emi@gmail.com", Password = EncryptPass.GetSHA256("admin"), Points = 50000, Role_Id = 1 },
            new User { Id = 4, First_name = "Vir", Last_name = "Schmied", Email = "vir@gmail.com", Password = EncryptPass.GetSHA256("admin"), Points = 50000, Role_Id = 1 },
            
            new User { Id = 5, First_name = "Pedro", Last_name = "Gonzalez", Email = "pedro@gmail.com", Password = EncryptPass.GetSHA256("user"), Points = 5800, Role_Id = 2 },
            new User { Id = 6, First_name = "Fede", Last_name = "Perez", Email = "fede@gmail.com", Password = EncryptPass.GetSHA256("user"), Points = 5040, Role_Id = 2 },
            new User { Id = 7, First_name = "Maca", Last_name = "Pereira", Email = "maca@gmail.com", Password = EncryptPass.GetSHA256("user"), Points = 1560, Role_Id = 2 },
            new User { Id = 8, First_name = "Sofi", Last_name = "Gomez", Email = "sofi@gmail.com", Password = EncryptPass.GetSHA256("user"), Points = 2300, Role_Id = 2 },
            
            // Users de cuentas bloqueadas
            new User { Id = 9, First_name = "Manu", Last_name = "Noriega", Email = "manu@gmail.com", Password = EncryptPass.GetSHA256("user"), Points = 1800, Role_Id = 2 },
            new User { Id = 10, First_name = "Clara", Last_name = "Aguayo", Email = "clara@gmail.com", Password = EncryptPass.GetSHA256("user"), Points = 2590, Role_Id = 2 }
        );
        
        // Seed Account
        modelBuilder.Entity<Account>().HasData(
            new Account { Id = 1, CreationDate = new DateTime(2018, 2, 21), Money = 210600, IsBlocked = false, UserId = 1 },
            new Account { Id = 2, CreationDate = new DateTime(2018, 8, 8), Money = 100980, IsBlocked = false, UserId = 2 },
            new Account { Id = 3, CreationDate = new DateTime(2019, 5, 15), Money = 250990, IsBlocked = false, UserId = 3 },
            new Account { Id = 4, CreationDate = new DateTime(2019, 11, 5), Money = 420560, IsBlocked = false, UserId = 4 },
            
            new Account { Id = 5, CreationDate = new DateTime(2020, 4, 10), Money = 80980, IsBlocked = false, UserId = 5 },
            new Account { Id = 6, CreationDate = new DateTime(2021, 7, 25), Money = 90900, IsBlocked = false, UserId = 6  },
            new Account { Id = 7, CreationDate = new DateTime(2022, 1, 30), Money = 100800, IsBlocked = false, UserId = 7  },
            new Account { Id = 8, CreationDate = new DateTime(2023, 11, 8), Money = 56630, IsBlocked = false, UserId = 8  },
            
            // Accounts bloqueadas
            new Account { Id = 9, CreationDate = new DateTime(2022, 6, 13), Money = 685510, IsBlocked = true, UserId = 9  },
            new Account { Id = 10, CreationDate = new DateTime(2023, 3, 15), Money = 550380, IsBlocked = true, UserId = 10  }
        );
        
        // Seed Transaction
        modelBuilder.Entity<Transaction>().HasData(
            new Transaction { transactionId = 1, Amount = 7500, Concept = "Compra en línea 3", Date = new DateTime(2023, 1, 15), Type = "payment", AccountId = 2, UserId = 2 },
            new Transaction { transactionId = 2, Amount = 3000, Concept = "Depósito en efectivo 2", Date = new DateTime(2023, 2, 5), Type = "topup", AccountId = 1, UserId = 1 },
            new Transaction { transactionId = 3, Amount = 9000, Concept = "Pago de factura 2", Date = new DateTime(2023, 3, 20), Type = "payment", AccountId = 3, UserId = 3, ToAccountId = 2 },
            new Transaction { transactionId = 4, Amount = 6000, Concept = "Transferencia a cuenta de terceros", Date = new DateTime(2023, 4, 10), Type = "payment", AccountId = 2, UserId = 2, ToAccountId = 3 },
            new Transaction { transactionId = 5, Amount = 4200, Concept = "Recarga de tarjeta 2", Date = new DateTime(2023, 5, 5), Type = "topup", AccountId = 4, UserId = 4 },
            new Transaction { transactionId = 6, Amount = 3000, Concept = "Transferencia a cuenta de terceros", Date = new DateTime(2023, 5, 10), Type = "payment", AccountId = 7, UserId = 7, ToAccountId = 5 },
            new Transaction { transactionId = 7, Amount = 3000, Concept = "Transferencia de terceros", Date = new DateTime(2023, 5, 10), Type = "payment", AccountId = 5, UserId = 5 },
            new Transaction { transactionId = 8, Amount = 7300, Concept = "Depósito", Date = new DateTime(2023, 9, 20), Type = "topup", AccountId = 6, UserId = 6 },
            new Transaction { transactionId = 9, Amount = 10500, Concept = "Depósito", Date = new DateTime(2023, 10, 10), Type = "topup", AccountId = 8, UserId = 8 },
            new Transaction { transactionId = 10, Amount = 8000, Concept = "Recarga de tarjeta", Date = new DateTime(2023, 11, 8), Type = "topup", AccountId = 7, UserId = 7 }
        );
        
        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.User)
            .WithMany(u => u.Transactions)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.NoAction); // O NoAction para OnUpdate
        
         // Seed Catalogue
        modelBuilder.Entity<Catalogue>().HasData(
            new Catalogue { Id = 1, ProductDescription = "Auriculares inalámbricos", Image = "https://th.bing.com/th/id/OIP.ZcSNf-JOOSvykdhsoXQ4VAHaHa?rs=1&pid=ImgDetMain", Points = 18000 },
            new Catalogue { Id = 2, ProductDescription = "Cafetera", Image = "https://www.officenter.com.uy/imgs/productos/productos31_92137.jpg", Points = 32000 },
            new Catalogue { Id = 3, ProductDescription = "Tarjeta de regalo de $50", Image = "https://facevital.com/cdn/shop/products/FV_giftcard50.jpg?v=1607884832&width=256", Points = 40000 },
            new Catalogue { Id = 4, ProductDescription = "SmartWatch", Image = "https://www.condorinformatica.uy/imgs/productos/productos31_12405.jpg", Points = 55000 }
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