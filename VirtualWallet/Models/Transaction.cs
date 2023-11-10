using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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
        public Account Account { get; set; }

        public int UserId { get; set; }  //FK a Users

        //public User User { get; set; }
        [ForeignKey("UserId")]
        //public User User { get; set; }
        
        public int? ToAccountId { get; set; } //FK a Account
        [ForeignKey("ToAccountId")]

        public Account ToAccount { get; set; }
    }
}
