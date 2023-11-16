using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualWallet.Models;

public class Account
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // ID auto incremental
    public int Id { get; set; }
    [Required]
    public DateTime CreationDate { get; set; }
    [Column(TypeName = "decimal(10, 2)")]
    [Required]
    public decimal Money { get; set; }
    [Required]
    public bool IsBlocked { get; set; }
    
    [Required]
    public int UserId { get; set; } // FK a Users
    [ForeignKey("UserId")]
    public virtual User User { get; set; }
}
