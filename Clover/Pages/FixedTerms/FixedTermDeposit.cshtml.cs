using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Clover.Pages
{
    public class FIxedTermDepositModel : PageModel
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

        //public async Task OnGetAsync()
        //{
        //    using (var httpClient = new HttpClient())
        //    {
        //        string token = HttpContext.Session.GetString("BearerToken");
        //        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        //        var fixedTermResponse = await httpClient.GetAsync($"http://localhost:7120/api/FixedTerm/GetAllMyFixedTerms?pageNumber={PageNumber}&pageSize={PageSize}");

        //        if (fixedTermResponse.IsSuccessStatusCode)
        //        {
        //            var jsonResponse = await fixedTermResponse.Content.ReadAsStringAsync();

        //            if (!string.IsNullOrWhiteSpace(jsonResponse))
        //            {
        //                FixedTermDeposit = await fixedTermResponse.Content.ReadFromJsonAsync<List<FixedTermDeposit>>();
        //            }
        //            else
        //            {
        //                FixedTermDeposit = new List<FixedTermDeposit>();
        //            }
        //        }
        //        else
        //        {
        //            FixedTermDeposit = new List<FixedTermDeposit>();
        //        }
        //    }
        //}

        public async Task OnGetAsync()
        {
            using (var httpClient = new HttpClient())
            {
                string token = HttpContext.Session.GetString("BearerToken");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var fixedTermResponse = await httpClient.GetAsync($"http://localhost:7120/api/FixedTerm/GetAllMyFixedTerms?pageNumber={PageNumber}&pageSize={PageSize}");
                var jsonResponse = await fixedTermResponse.Content.ReadAsStringAsync();

                if (fixedTermResponse.IsSuccessStatusCode)
                {
                    var apiResponse = JsonConvert.DeserializeObject<FixedTermResponse>(jsonResponse);

                    if (apiResponse != null)
                    {
                        FixedTermDeposit = apiResponse.FixedTermDeposits;
                        PrevPage = apiResponse.PrevPage;
                        NextPage = apiResponse.NextPage;
                        TotalPages = apiResponse.TotalPages;
                    }
                    else
                    {
                        FixedTermDeposit = new List<FixedTermDeposit>();
                    }
                }
                else
                {
                    FixedTermDeposit = new List<FixedTermDeposit>();
                }
            }
        }
        //public async Task<IActionResult> OnPostCreateFixedTerm()
        //{
        //    using (var httpClient = new HttpClient())
        //    {
        //        string token = HttpContext.Session.GetString("BearerToken");
        //        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        //        string AccountId = Request.Form["AccountId"];
        //        string CreationDate = Request.Form["CreationDate"];
        //        string ClosingDate = Request.Form["ClosingDate"];
        //        string NominalRate = Request.Form["NominalRate"];
        //        string State = Request.Form["State"];
        //        string Amount = Request.Form["Amount"];

        //        var data = new
        //        {
        //            AccountId = AccountId,
        //            CreationDate = CreationDate,
        //            ClosingDate = ClosingDate,
        //            NominalRate=NominalRate,
        //            State=State,
        //            Amount=Amount
        //        };


        //        var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
        //        var response = await httpClient.PostAsync($"http://localhost:7120/api/FixedTerm/PostMyNewFixedTerm/", content);

        //        var FixedTermResponse = await httpClient.GetAsync("http://localhost:7120/api/FixedTerm");

        //        if (FixedTermResponse.IsSuccessStatusCode)
        //        {
        //            var apiResponse = await FixedTermResponse.Content.ReadFromJsonAsync<FixedTermResponse>();

        //            if (apiResponse != null)
        //            {
        //                FixedTermDeposit = apiResponse.FixedTermDeposits;
        //                PrevPage = apiResponse.PrevPage;
        //                NextPage = apiResponse.NextPage;
        //            }

        //            await OnGetAsync();

        //            //else
        //            //{
        //            //    FixedTermDepositList = new List<FixedTermDeposit>();
        //            //}
        //        }

        //    }
        //    return Page();


        //}
    }
  
}

public class FixedTermDeposit
{
    public int Id { get; set; }

    [BindNever]
    public int UserId { get; set; }

    public virtual User User { get; set; }

    public int AccountId { get; set; }


    public decimal Amount { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime ClosingDate { get; set; }

    public decimal NominalRate { get; set; }

    public string State { get; set; }
}

public class FixedTermResponse
{
    public List<FixedTermDeposit> FixedTermDeposits { get; set; }
    public object PrevPage { get; set; }
    public object NextPage { get; set; }

    public int TotalPages { get; set; }
}

