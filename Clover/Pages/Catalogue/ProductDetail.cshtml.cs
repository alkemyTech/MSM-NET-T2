using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Clover.Pages.Catalogue
{

    public class ProductDetailModel : PageModel
    {
        [BindProperty]
        public Catalogue catalogue { get; set; } = new Catalogue();
        public async Task OnGetAsync(int id)
        {

            using (var httpClient = new HttpClient())
            {
                string token = HttpContext.Session.GetString("BearerToken");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);



                httpClient.BaseAddress = new Uri("http://localhost:7120/api/Catalogue");
                catalogue = await httpClient.GetFromJsonAsync<Catalogue>($"Catalogue/{id}");

            }
        }
    }
}
