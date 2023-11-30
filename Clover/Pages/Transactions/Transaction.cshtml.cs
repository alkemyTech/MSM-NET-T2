using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Clover.Pages.Transactions
{
    public class TransactionModel : PageModel
    {
        [BindProperty]
        public Transaction Transaction { get; set; } = new Transaction();
        public List<Transaction> TransactionsList { get; set; }

        public List<User> UsersList { get; set; }

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
                string token = HttpContext.Session.GetString("BearerToken");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var transactionResponse = await httpClient.GetAsync($"http://localhost:7120/api/Transaction?pageNumber={PageNumber}&pageSize={PageSize}");

                if (transactionResponse.IsSuccessStatusCode)
                {
                    var apiResponse = await transactionResponse.Content.ReadFromJsonAsync<TransactionResponse>();

                    if (apiResponse != null)
                    {
                        TransactionsList = apiResponse.Transactions;
                        PrevPage = apiResponse.PrevPage;
                        NextPage = apiResponse.NextPage;
                        TotalPages = apiResponse.TotalPages;
                    }
                    else
                    {
                        TransactionsList = new List<Transaction>();
                    }
                }
                else
                {
                    TransactionsList = new List<Transaction>();
                }
            }
        }
        public async Task<IActionResult> OnPostTransfer()
        {
            using (var httpClient = new HttpClient())
            {
                string token = HttpContext.Session.GetString("BearerToken");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                Transaction.Date = DateTime.Now;

                var content = new StringContent(JsonConvert.SerializeObject(Transaction), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync($"http://localhost:7120/api/Account/Transfer/{Transaction.UserId}", content);

                var transactionResponse = await httpClient.GetAsync("http://localhost:7120/api/Transaction");

                if (transactionResponse.IsSuccessStatusCode)
                {
                    var apiResponse = await transactionResponse.Content.ReadFromJsonAsync<TransactionResponse>();

                    if (apiResponse != null)
                    {
                        TransactionsList = apiResponse.Transactions;
                        PrevPage = apiResponse.PrevPage;
                        NextPage = apiResponse.NextPage;
                    }
                    else
                    {
                        TransactionsList = new List<Transaction>();
                    }
                }
                return Redirect("/Transactions");
            }
            return Page();


        }

        public async Task<IActionResult> OnPostDeposit()
        {
            using (var httpClient = new HttpClient())
            {
                string token = HttpContext.Session.GetString("BearerToken");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                Transaction.Date = DateTime.Now;

                var content = new StringContent(JsonConvert.SerializeObject(Transaction), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync($"http://localhost:7120/api/Account/Deposit/{Transaction.UserId}", content);

                var transactionResponse = await httpClient.GetAsync("http://localhost:7120/api/Transaction");

                if (transactionResponse.IsSuccessStatusCode)
                {
                    var apiResponse = await transactionResponse.Content.ReadFromJsonAsync<TransactionResponse>();

                    if (apiResponse != null)
                    {
                        TransactionsList = apiResponse.Transactions;
                        PrevPage = apiResponse.PrevPage;
                        NextPage = apiResponse.NextPage;
                    }
                    else
                    {
                        TransactionsList = new List<Transaction>();
                    }
                }
                return Redirect("/Transactions");

            }
            return Page();


        }
    }
}

public class Transaction
{
    public int TransactionId { get; set; }
    public decimal Amount { get; set; }
    public string Concept { get; set; }
    public DateTime Date { get; set; }
    public string Type { get; set; }
    public int AccountId { get; set; } //FK a Account
    public int UserId { get; set; }  //FK a Users
    public int? ToAccountId { get; set; }
}

public class TransactionResponse
{
    public List<Transaction> Transactions { get; set; }
    public object PrevPage { get; set; }
    public object NextPage { get; set; }

    public int TotalPages { get; set; }
}
public class ResponseUser
{
    public List<User> Users { get; set; }
    public object PrevPage { get; set; }
    public object NextPage { get; set; }


}

