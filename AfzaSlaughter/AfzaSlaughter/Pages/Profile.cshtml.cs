using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AfzaSlaughter.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace AfzaSlaughter.Pages
{
    public class ProfileModel : PageModel
    {
        public const string EmailSession = "_Email";
        public const string NameSession = "_Name";
        public const string UsernameSession = "_Username";

        [BindProperty(SupportsGet = true)]
        public User user { get; set; }

        [BindProperty(SupportsGet = true)]
        public string username { get; set; }

        [BindProperty(SupportsGet = true)]
        public string name { get; set; }

        [BindProperty(SupportsGet = true)]
        public string email { get; set; }

        [BindProperty(SupportsGet = true)]
        public string password { get; set; }

        [BindProperty(SupportsGet = true)]
        public string contact { get; set; }

        [BindProperty(SupportsGet = true)]
        public string address { get; set; }

        [BindProperty(SupportsGet = true)]
        public string notAllowed { get; set; } = "You need sign up or login";
        public string LogoutMsg { get; set; } = "You have successfully Signout";


        public IActionResult OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(EmailSession)))
            {
                //fname = "Your First name is missing";
                return RedirectToPage("/Signup", new { notAllowed });
            }
            else
            {
                username = HttpContext.Session.GetString(UsernameSession);
                name = HttpContext.Session.GetString(NameSession);
                email = HttpContext.Session.GetString(EmailSession);

                return Page();
            }

        }
        public IActionResult OnPost()
        {
            SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AfzaSlaughter;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            string hash = BitConverter.ToString(MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(user.password))).Replace("-", "");
            string UpdateQuery = "UPDATE [AfzaSlaughter].[dbo].[User] Set UserName= '" + user.username + "', Name = '" + user.name + "', Contact = '" + user.contact + "', Address = '" + user.address + "', Password = '" + hash + "' WHERE Email = ('" + user.email + "')";
            SqlCommand cmd = new SqlCommand(UpdateQuery, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return RedirectToPage("/Profile", new { user.username, user.name, user.contact, user.address, user.email });
        }
    }
}
