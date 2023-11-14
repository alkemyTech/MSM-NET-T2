using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VirtualWallet.Models.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Points { get; set; }
        public int Role_Id { get; set; }

    }
}
