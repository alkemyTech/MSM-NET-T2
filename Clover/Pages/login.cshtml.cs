using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Clover.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public LoginModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:7120/api/Autenticacion/validar");
            request.Content = new StringContent(JsonConvert.SerializeObject(Input), Encoding.UTF8, "application/json");

            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseContent);
                var responseObject = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseContent);
                Console.WriteLine(responseObject);
                var token = responseObject["token"];
                var id = responseObject["id"];

                HttpContext.Session.SetString("BearerToken", token);
                HttpContext.Session.SetString("UserId", id);

                TempData["Id"] = id;
                TempData.Keep("Id");
                TempData["Token"] = token;
                TempData.Keep("Token");
                return LocalRedirect(Url.Content("/User"));
            }
            else
            {
                ModelState.AddModelError("Error", "Verifique su email y/o contraseña.");
                return Page();
            }


        }
    }

}
