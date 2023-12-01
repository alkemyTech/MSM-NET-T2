using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Clover.Pages
{
    public class Index1Model : PageModel
    {
        [BindProperty]
        public UserRegister Usuario { get; set; }
        public async Task<IActionResult> OnPost()
        {
            using (var httpClient = new HttpClient())
            {
                
                var content = new StringContent(JsonConvert.SerializeObject(Usuario), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync($"http://localhost:7120/api/User/", content);


                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage("/login");
                    
                }

            }
            return Page();


        }
    }
}

public class UserRegister
{
    public string First_name { get; set; }
    public string Last_name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
} 