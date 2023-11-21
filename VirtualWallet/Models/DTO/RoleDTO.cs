using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace VirtualWallet.Models.DTO;

public class RoleDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}