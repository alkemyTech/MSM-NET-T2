using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Clover.Pages.Accounts;

public class AccountModel : PageModel
{
    public List<Account> AccountList { get; set; }

    [BindProperty(SupportsGet = true)]
    public int PageNumber { get; set; } = 1;

    [BindProperty(SupportsGet = true)]
    public int PageSize { get; set; } = 10;

    public object PrevPage { get; private set; }
    public object NextPage { get; private set; }
    public int TotalPages { get; set; }

    public async Task OnGetAsync()
    {
        using (var httpClient = new HttpClient())
        {
            var role = HttpContext.Session.GetString("Role");
            ViewData["Role"] = role;

            string token = HttpContext.Session.GetString("BearerToken");

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.GetAsync("http://localhost:7120/api/Account");
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadFromJsonAsync<AccountResponse>();

                if (apiResponse != null)
                {
                    AccountList = apiResponse.Accounts;
                    PrevPage = apiResponse.PrevPage;
                    NextPage = apiResponse.NextPage;
                    TotalPages = apiResponse.TotalPages;
                }
                else
                {
                    AccountList = new List<Account>();
                }
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
            string token = HttpContext.Session.GetString("BearerToken");

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


            var content = new StringContent(JsonConvert.SerializeObject(Account), Encoding.UTF8, "application/json"); 
            var response = await httpClient.PostAsync("http://localhost:7120/api/Account", content);

            var accountResponse = await httpClient.GetAsync("http://localhost:7120/api/Account");

            if (accountResponse.IsSuccessStatusCode)
            {
                var apiResponse = await accountResponse.Content.ReadFromJsonAsync<AccountResponse>();

                    if (apiResponse != null)
                    {
                        AccountList = apiResponse.Accounts;
                        PrevPage = apiResponse.PrevPage;
                        NextPage = apiResponse.NextPage;
                    }
                    else
                    {
                        AccountList = new List<Account>();
                    }
                return Redirect("/Account");
            }
            
        }
        return Page();
    }
    
}
public class Account
{
    public int Id { get; set; }
    public DateTime CreationDate { get; set; }
    public decimal Money { get; set; }
    public bool IsBlocked { get; set; }
    public int UserId { get; set; }
}


public class AccountResponse
{
    public List<Account> Accounts { get; set; }
    public object PrevPage { get; set; }
    public object NextPage { get; set; }

    public int TotalPages { get; set; }
}