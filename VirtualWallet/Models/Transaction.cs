using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VirtualWallet.Models
{
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int transactionId { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal amount { get; set; }

        [MaxLength(255)]
        public string concept { get; set; }

        public DateTime date { get; set; }

        [MaxLength(255)]
        public string type { get; set; } //Topuop, payment

        [ForeignKey("Account")]
        public int accountId { get; set; } //FK a Account
        public Account Account { get; set; }

        [ForeignKey("User")]
        public int userId { get; set; }  //FK a Users
        public User User { get; set; }

        [ForeignKey("ToAccount")]
        public int? toAccountId { get; set; } //FK a Account
        public Account ToAccount { get; set; }
    }
}
