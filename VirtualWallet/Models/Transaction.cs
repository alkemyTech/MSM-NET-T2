namespace VirtualWallet.Models
{
    public class Transaction
    {
        public int transaction_id { get; set; }
        public decimal amount { get; set; }
        public string concept { get; set; }
        public DateTime date { get; set; }
        public string type { get; set; } //Topuop, payment
        public int account_id { get; set; } //FK a Account
        public int user_id { get; set; }  //FK a Users
        public int? to_account_id { get; set; } //FK a Account

        //Propiedades
        public Account account { get; set; }
        public User user { get; set; }
        public Account ToAccount { get; set; }
    }
}
