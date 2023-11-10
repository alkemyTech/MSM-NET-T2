using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualWallet.Models
{
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int transactionId { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal Amount { get; set; }

        [MaxLength(255)]
        public string Concept { get; set; }

        public DateTime Date { get; set; }

        [MaxLength(255)]
        public string Type { get; set; } //Topuop, payment

        public int AccountId { get; set; } //FK a Account
        [ForeignKey("AccountId")]
        public virtual Account Account { get; set; }

        public int UserId { get; set; }  //FK a Users
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public int? ToAccountId { get; set; } //FK a Account
        
    }
}
