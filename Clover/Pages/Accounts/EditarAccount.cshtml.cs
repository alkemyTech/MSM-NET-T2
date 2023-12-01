using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Clover.Pages.Accounts
{
    public class EditarAccountModel : PageModel
    {
        [BindProperty]
        public Account Account { get; set; } = new Account();

        public async Task OnGetAsync(int id)
        {

            using (var httpClient = new HttpClient())
            {
                string token = HttpContext.Session.GetString("BearerToken");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);



                httpClient.BaseAddress = new Uri("http://localhost:7120/api/Account");
                Account = await httpClient.GetFromJsonAsync<Account>($"Account/{id}");

                HttpContext.Session.SetInt32("AccountId", Account.Id);

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
                Account.CreationDate = DateTime.Now;

                string token = HttpContext.Session.GetString("BearerToken");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                httpClient.BaseAddress = new Uri("http://localhost:7120/api/Account");
                HttpResponseMessage response = await httpClient.PutAsJsonAsync($"Account/{Account.Id}", Account);

                if (response.IsSuccessStatusCode)
                {
                    return Redirect("/account");
                }
                else
                {
                    return Page();
                }
            }
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            int? accountId = HttpContext.Session.GetInt32("AccountId");

            using (var httpClient = new HttpClient())
            {
                string token = HttpContext.Session.GetString("BearerToken");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                httpClient.BaseAddress = new Uri("http://localhost:7120/api/Account");
                HttpResponseMessage response = await httpClient.DeleteAsync($"Account/{accountId}");

                if (response.IsSuccessStatusCode)
                {
                    return Redirect("/account");
                }
                else
                {
                    return Page();
                }
            }
        }
    }
}