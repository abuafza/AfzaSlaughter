using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AfzaSlaughter.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AfzaSlaughter.Pages
{
    public class LogoutModel : PageModel
    {
        public const string EmailSession = "_Email";
        public const string NameSession = "_Name";
        public const string UsernameSession = "_Username";

        [BindProperty(SupportsGet = true)]
        public User user { get; set; }

        public string LogoutMsg { get; set; } = "You have successfully Logout";
        public IActionResult OnGet()
        {
            HttpContext.Session.SetString(EmailSession, "");
            HttpContext.Session.SetString(NameSession, "");
            HttpContext.Session.SetString(UsernameSession, "");

            return RedirectToPage("/Index", new { LogoutMsg });
        }
    }
}
