using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Clover.Pages.Accounts
{
    public class MyAccountModel : PageModel
    {
        public Account MyAccount { get; set; } = new Account();

        public async Task OnGetAsync()
        {
            var role = HttpContext.Session.GetString("Role");
            ViewData["Role"] = role;

            using (var httpClient = new HttpClient())
            {
                string id = HttpContext.Session.GetString("UserId");

                string token = HttpContext.Session.GetString("BearerToken");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);



                httpClient.BaseAddress = new Uri("http://localhost:7120/api/Account");
                MyAccount = await httpClient.GetFromJsonAsync<Account>($"Account/{id}");

            }
        }
    }
}
