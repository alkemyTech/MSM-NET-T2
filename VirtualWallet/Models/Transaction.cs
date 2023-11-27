using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace VirtualWallet.Models
{
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int transactionId { get; set; }

        [Required(ErrorMessage = "El campo Amount es obligatorio.")]

        [Column(TypeName = "decimal(10, 2)")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "El campo Concept es obligatorio.")]
        [MaxLength(255)]
        public string Concept { get; set; }

        [Required(ErrorMessage = "El campo Date es obligatorio.")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "El campo Type es obligatorio.")]
        [MaxLength(255)]
        public string Type { get; set; } //Topuop, payment

        [Required(ErrorMessage = "El campo AccountId es obligatorio.")]
        public int AccountId { get; set; } //FK a Account
        [ForeignKey("AccountId")]
        [JsonIgnore]
        public virtual Account? Account { get; set; }

        [Required(ErrorMessage = "El campo UserId es obligatorio.")]
        public int UserId { get; set; }  //FK a Users
        [ForeignKey("UserId")]
        [JsonIgnore]
        public virtual User? User { get; set; }

        public int? ToAccountId { get; set; } //FK a Account

    }
}
