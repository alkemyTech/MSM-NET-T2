using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Clover.Pages.Catalogue
{
    
    public class CatalogueModel : PageModel
    {
        [BindProperty]
        public Catalogue catalogue { get; set; } = new Catalogue();

        public List<Catalogue> CatalogueList { get; set; }
        public async Task OnGetAsync()
        {
            using (var httpClient = new HttpClient())
            {
                string token = HttpContext.Session.GetString("BearerToken");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var catalogueResponse = await httpClient.GetAsync($"http://localhost:7120/api/Catalogue");

                if (catalogueResponse.IsSuccessStatusCode)
                {
                    var json = await catalogueResponse.Content.ReadAsStringAsync();
                    CatalogueList = JsonConvert.DeserializeObject<List<Catalogue>>(json);
                }
            }
        }
    }


    public class Catalogue
    {
        public int id { get; set; }

        public string productDescription { get; set; }

        public string image { get; set; }

        public int points { get; set; }
    }
}
