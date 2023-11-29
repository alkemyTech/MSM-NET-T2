using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;

namespace Clover.Pages
{
    public class UserModel : PageModel
    {
        [BindProperty]
        public UsuarioEncontrado UsuarioEncontrado { get; set; } = new UsuarioEncontrado();
        public UsuarioEncontrado Encontrado { get; set; }
        public async Task OnGetAsync()
        {
            using (var httpClient = new HttpClient())
            {
                string id = HttpContext.Session.GetString("UserId");
                string token = HttpContext.Session.GetString("BearerToken");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var userResponse = await httpClient.GetAsync($"http://localhost:7120/api/User/{id}");

                if (userResponse.IsSuccessStatusCode)
                {
                    var users = await userResponse.Content.ReadFromJsonAsync<UsuarioEncontrado>();
                    Console.WriteLine(users);
                    if (users != null)
                    {
                        Encontrado = users;
                    }
                    else
                    {
                        UsuarioEncontrado = UsuarioEncontrado;
                    }
                }
                else
                {
                    UsuarioEncontrado = UsuarioEncontrado;
                }
            }
        }
    }
}
public class UsuarioEncontrado
{
    public int Id { get; set; }
    public string First_name { get; set; }
    public string Last_name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int Points { get; set; }
    public int Role_Id { get; set; }
    public object Role { get; set; }
    public object FixedTermDeposits { get; set; }
    public object Transactions { get; set; }
}