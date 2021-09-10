using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AfzaSlaughter.Pages
{
    public class ContactModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string result { get; set; }

        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://nexmo-nexmo-sms-verify-v1.p.rapidapi.com/send-verification-code?brand=AFZA%20IT&phoneNumber=16477803079"),
                Headers =
    {
        { "x-rapidapi-host", "nexmo-nexmo-sms-verify-v1.p.rapidapi.com" },
        { "x-rapidapi-key", "0862fa48bbmshbf4dddfff34c02dp175413jsnaf73410531c8" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();

                result = "Thanks for sending text. We will contact you as soon as possible.";
                return Page();
            }

        }
    }
}
