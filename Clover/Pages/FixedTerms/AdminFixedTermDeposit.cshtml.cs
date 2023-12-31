using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Headers;
using Clover.Pages.Accounts;

namespace Clover.Pages.FixedTerms
{
    public class AdminFixedTermDepositModel : PageModel
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

        public async Task OnGetAllAsync()
        {
            using (var httpClient = new HttpClient())
            {

                var role = HttpContext.Session.GetString("Role");
                ViewData["Role"] = role;

                string token = HttpContext.Session.GetString("BearerToken");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var fixedTermResponse = await httpClient.GetAsync($"http://localhost:7120/api/FixedTerm/GetAll?pageNumber={PageNumber}&pageSize={PageSize}");

                if (fixedTermResponse.IsSuccessStatusCode)
                {
                    var jsonResponse = await fixedTermResponse.Content.ReadFromJsonAsync<FixedTermResponse>();

                    if (jsonResponse != null)
                    {
                        FixedTermDeposit = jsonResponse.FixedTerm;
                        PrevPage = jsonResponse.PrevPage;
                        NextPage = jsonResponse.NextPage;
                        TotalPages = jsonResponse.TotalPages;
                    }
                }
                else
                {
                    FixedTermDeposit = new List<FixedTermDeposit>();
                }
            }
        }


        //public async Task<IActionResult> OnPostDeleteAsync(int id)
        //{
        //    //int? fixedTermId = HttpContext.Session.GetInt32("Id");

        //    using (var httpClient = new HttpClient())
        //    {
        //        string token = HttpContext.Session.GetString("BearerToken");
        //        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        //        //httpClient.BaseAddress = new Uri("http://localhost:7120/api/FixedTerm/DeleteMyFixedTerm/{id}");
        //        var response = await httpClient.DeleteAsync($"http://localhost:7120/api/FixedTerm/DeleteMyFixedTerm/{id}");

        //        if (response.IsSuccessStatusCode)
        //        {
        //            return Redirect("/FixedTerms/FixedTermDeposit");
        //        }
        //        else
        //        {
        //            return Page();
        //        }
        //    }
        //}


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
}
