using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;

namespace Clover.Pages
{
    public class DashboardModel : PageModel
    {
        public List<User> ListUsers { get; set; }

        public async Task OnGetAsync()
        {
            using (var httpClient = new HttpClient())
            {
                string token = HttpContext.Session.GetString("BearerToken");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


                var response = await httpClient.GetAsync("http://localhost:7120/api/User?pageNumber=1&pageSize=10");
                Console.WriteLine(response);
                if (response.IsSuccessStatusCode)
                {
                    ListUsers = await response.Content.ReadFromJsonAsync<List<User>>();
                    Console.WriteLine(ListUsers);
                }
                else
                {
                    ListUsers = new List<User>();
                }
            }
        }
    }
}
public class User
{

    public string First_name { get; set; }
    public string Last_name { get; set; }
    public string Email { get; set; }
    public int Points { get; set; }
    public int Role_Id { get; set; }

}
