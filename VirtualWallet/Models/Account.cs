using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualWallet.Models;

public class Account
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // ID auto incremental
    public int Id { get; set; }
    public DateTime CreationDate { get; set; }
    
    [Column(TypeName = "decimal(10, 2)")]
    public decimal Money { get; set; }
    public bool IsBlocked { get; set; }
    
    public int UserId { get; set; }
    [ForeignKey("UserId")]
    public virtual User User { get; set; }
}
