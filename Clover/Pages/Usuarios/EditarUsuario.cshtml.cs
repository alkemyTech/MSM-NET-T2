using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Clover.Pages.Usuarios
{
    public class EditarUsuarioModel : PageModel
    {
        [BindProperty]
        public User user { get; set; } = new User();

        public async Task OnGet(int id)
        {
            using (var httpClient = new HttpClient())
            {
                string token = HttpContext.Session.GetString("BearerToken");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                httpClient.BaseAddress = new Uri("http://localhost:7120/api/User");
                user = await httpClient.GetFromJsonAsync<User>($"User/{id}");
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
                string token = HttpContext.Session.GetString("BearerToken");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                httpClient.BaseAddress = new Uri("http://localhost:7120/api/User");
                HttpResponseMessage response = await httpClient.PutAsJsonAsync($"User/{user.Id}", user);

                if (response.IsSuccessStatusCode)
                {
                    return Redirect("/Index");
                }
                else
                {
                    return Page();
                }
            }
        }
        public async Task<IActionResult> OnPostDelete()
        {
            using (var httpClient = new HttpClient())
            {
                string token = HttpContext.Session.GetString("BearerToken");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                httpClient.BaseAddress = new Uri("http://localhost:7120/api/User");
                HttpResponseMessage response = await httpClient.DeleteAsync($"User/{user.Id}");

                if (response.IsSuccessStatusCode)
                {
                    return Redirect("/Index");
                }
                else
                {
                    return Page();
                }
            }
        }

    }
    
}

public class User
{
    public int Id { get; set; }
    public string First_name { get; set; }
    public string Last_name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int Points { get; set; }
    public int Role_Id { get; set; }
}