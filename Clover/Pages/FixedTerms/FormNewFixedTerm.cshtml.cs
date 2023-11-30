using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using System.Text;

namespace Clover.Pages.FixedTerms
{
    public class FormNewFixedTermModel : PageModel
    {
        [BindProperty]
        public FixedTermDeposit fixedTermDeposit { get; set; } = new FixedTermDeposit();
        public List<FixedTermDeposit>? FixedTermDeposit { get; set; }

        public List<User> UsersList { get; set; }

        [BindProperty(SupportsGet = true)]

        public int PageNumber { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; } = 10;

        public object PrevPage { get; private set; }
        public object NextPage { get; private set; }

        public int TotalPages { get; set; }
        public async Task<IActionResult> OnPostCreateFixedTerm(FixedTermDeposit fixedTermDeposit)
        {
            using (var httpClient = new HttpClient())
            {
                string token = HttpContext.Session.GetString("BearerToken");

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


                var content = new StringContent(JsonConvert.SerializeObject(fixedTermDeposit), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync("http://localhost:7120/api/FixedTerm/PostMyNewFixedTerm", content);

                var fixedTermresponse = await httpClient.GetAsync($"http://localhost:7120/api/FixedTerm/GetAllMyFixedTerms");

                if (fixedTermresponse.IsSuccessStatusCode)
                {
                    var apiResponse = await fixedTermresponse.Content.ReadFromJsonAsync<FixedTermResponse>();

                    if (apiResponse != null)
                    {
                        FixedTermDeposit = apiResponse.FixedTerm;
                        PrevPage = apiResponse.PrevPage;
                        NextPage = apiResponse.NextPage;

                    }
                    else
                    {
                        FixedTermDeposit = new List<FixedTermDeposit>();
                    }
                    return Redirect("./FixedTermDeposit");
                }

            }
            return Page();
        }

    }
}

public class FixedTermDeposit
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [BindNever]
    public int Id { get; set; }

    [BindNever]
    public int UserId { get; set; }

    public virtual User User { get; set; }

    public int AccountId { get; set; }


    public decimal Amount { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime ClosingDate { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal NominalRate { get; set; }


    public string State { get; set; }
}

public class FixedTermResponse
{
    public List<FixedTermDeposit> FixedTerm { get; set; }
    public object PrevPage { get; set; }
    public object NextPage { get; set; }

    public int TotalPages { get; set; }
}
