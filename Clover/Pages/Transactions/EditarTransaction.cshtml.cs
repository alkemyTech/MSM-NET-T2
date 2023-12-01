using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Clover.Pages.Transactions
{
    //[Authorize(Roles = "Admin")]
    public class EditarTransactionModel : PageModel
    {
        [BindProperty]
        public Transaction Transaction { get; set; } = new Transaction();


        
        public async Task OnGetAsync(int id)
        {

            using (var httpClient = new HttpClient())
            {
                string token = HttpContext.Session.GetString("BearerToken");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);



                httpClient.BaseAddress = new Uri("http://localhost:7120/api/Transaction");
                Transaction = await httpClient.GetFromJsonAsync<Transaction>($"Transaction/{id}");

                HttpContext.Session.SetInt32("TransactionId", Transaction.TransactionId);

            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            using (var httpClient = new HttpClient())
            {
                Transaction.Date = DateTime.Now;

                string token = HttpContext.Session.GetString("BearerToken");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                httpClient.BaseAddress = new Uri("http://localhost:7120/api/Transaction");
                HttpResponseMessage response = await httpClient.PutAsJsonAsync($"Transaction/{Transaction.TransactionId}", Transaction);

                if (response.IsSuccessStatusCode)
                {
                    return Redirect("/Transactions");
                }
                else
                {
                    return Page();
                }
            }
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            int? transactionId = HttpContext.Session.GetInt32("TransactionId");

            using (var httpClient = new HttpClient())
            {
                string token = HttpContext.Session.GetString("BearerToken");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                httpClient.BaseAddress = new Uri("http://localhost:7120/api/Transaction");
                HttpResponseMessage response = await httpClient.DeleteAsync($"Transaction/{transactionId}");

                if (response.IsSuccessStatusCode)
                {
                    return Redirect("/Transactions");
                }
                else
                {
                    return Page();
                }
            }
        }
    }
}

