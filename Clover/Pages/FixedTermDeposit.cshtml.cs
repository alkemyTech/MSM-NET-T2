using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.Text;

namespace Clover.Pages
{
    public class FIxedTermDepositModel : PageModel
    {
        public List<FixedTermDeposit> FixedTermDeposit { get; set; }
        public async Task OnGetAsync()
        {
            using (var HttpClient = new HttpClient())
            {
                var response = await HttpClient.GetAsync("http://localhost:7120/api/FixedTerm/GetAllMyFixedTerms");

                if (response.IsSuccessStatusCode)
                {
                    FixedTermDeposit = await response.Content.ReadFromJsonAsync<List<FixedTermDeposit>>();
                }
                else
                {
                    FixedTermDeposit = new List<FixedTermDeposit>();
                }
            }

        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            using (var HttpClient = new HttpClient())
            {
                var response = await HttpClient.DeleteAsync($"http://localhost:7120/api/FixedTerm/" + id);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage("/FixedTermDeposit");
                }
                else
                {
                    return Page();
                }
            }
        }
    }

    public class FixedTermDeposit
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int AccountId { get; set; }

        public decimal Amount { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ClosingDate { get; set; }

        public decimal NominalRate { get; set; }

        public string State { get; set; }
    }
}

