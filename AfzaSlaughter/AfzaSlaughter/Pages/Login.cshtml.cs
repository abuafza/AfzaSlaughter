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
    public class LoginModel : PageModel
    {
        public const string EmailSession = "_Email";
        public const string NameSession = "_Name";
        public const string UsernameSession = "_Username";

        [BindProperty(SupportsGet = true)]
        public User user { get; set; }

        [BindProperty(SupportsGet = true)]
        public string error { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            string hash = BitConverter.ToString(MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(user.password))).Replace("-", "");
            SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AfzaSlaughter;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            string LoginQuery = "SELECT * FROM [AfzaSlaughter].[dbo].[User] where Email = '" + user.email.ToLower() + "' AND Password = '" + hash + "'";
            SqlCommand ReadCommand = new SqlCommand(LoginQuery, con);
            con.Open();
            SqlDataReader LoginReader = ReadCommand.ExecuteReader();
            if (LoginReader.Read())//like if(reader.Read() == true)
            {
                user.username = string.Format("{0}", LoginReader[1]);
                HttpContext.Session.SetString(UsernameSession, user.username);
                HttpContext.Session.SetString(EmailSession, user.email);
                return RedirectToPage("/Profile", new { user.username });
                }
                else
                {
                    error = "We did not find that user. Make sure your email/password is correct or create a new user!";
                    return Page();
                }
        }
    }
}
