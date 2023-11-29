using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Clover.Pages.Accounts;

public class AccountModel : PageModel
{
    public List<Account> AccountList { get; set; }
    
    public async Task OnGetAsync()
    {
        using (var HttpClient = new HttpClient())
        {
            var response = await HttpClient.GetAsync("http://localhost:7120/api/Account");
        
            if (response.IsSuccessStatusCode)
            {
                AccountList = await response.Content.ReadFromJsonAsync<List<Account>>();
            }
            else
            {
                AccountList = new List<Account>();
            }
        }
    }
    
    public async Task<IActionResult> OnPostAsync(Account Account)
    {
        using (var httpClient = new HttpClient())
        {
            var content = new StringContent(JsonConvert.SerializeObject(Account), Encoding.UTF8, "application/json"); 
            var response = await httpClient.PostAsync("http://localhost:7221/Account", content);
            var getResponse = await httpClient.GetAsync("http://localhost:7221/Account");
        
            if (response.IsSuccessStatusCode)
            {
                AccountList = await response.Content.ReadFromJsonAsync<List<Account>>();
            }
            else
            {
                AccountList = new List<Account>();
            }
        }
        return RedirectToPage("/Accounts");
    }
    public class Account
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public decimal Money { get; set; }
        public bool IsBlocked { get; set; }
        public int UserId { get; set; }
    }
}