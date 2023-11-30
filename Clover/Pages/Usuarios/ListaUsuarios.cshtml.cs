using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace Clover.Pages.Usuarios
{
    public class ListaUsuariosModel : PageModel
    {
        [BindProperty]
        public Usuario Usuario { get; set; } = new Usuario();
        public List<Usuario> ListUsers { get; set; }

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

                var userResponse = await httpClient.GetAsync($"http://localhost:7120/api/User?pageNumber={PageNumber}&pageSize={PageSize}");

                if (userResponse.IsSuccessStatusCode)
                {
                    var users = await userResponse.Content.ReadFromJsonAsync<UserResponse>();

                    if (users != null)
                    {
                        ListUsers = users.Users;
                        PrevPage = users.PrevPage;
                        NextPage = users.NextPage;
                        TotalPages = users.TotalPages;
                    }
                    else
                    {
                        ListUsers = new List<Usuario>();
                    }
                }
                else
                {
                    ListUsers = new List<Usuario>();
                }
            }
        }
        //public async Task<IActionResult> OnPostUser()
        //{
        //    using (var httpClient = new HttpClient())
        //    {
        //        string token = HttpContext.Session.GetString("BearerToken");
        //        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        //        var content = new StringContent(JsonConvert.SerializeObject(Usuario), Encoding.UTF8, "application/json");
        //        var response = await httpClient.PostAsync($"http://localhost:7120/api/User", content);

        //        var userResponse = await httpClient.GetAsync("http://localhost:7120/api/User");

        //        if (userResponse.IsSuccessStatusCode)
        //        {
        //            var apiResponse = await userResponse.Content.ReadFromJsonAsync<UserResponse>();

        //            if (apiResponse != null)
        //            {
        //                ListUsers = apiResponse.Users;
        //                PrevPage = apiResponse.PrevPage;
        //                NextPage = apiResponse.NextPage;
        //            }
        //            else
        //            {
        //                ListUsers = new List<Usuario>();
        //            }
        //        }

        //    }
        //    return RedirectToPage("/dashboard");


        //}
    }
}
public class Usuario
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
public class UserResponse
{
    public List<Usuario> Users { get; set; }
    public object PrevPage { get; set; }
    public object NextPage { get; set; }
    public int TotalPages { get; set; }
}
