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
    public class SignupModel : PageModel
    {
        public const string EmailSession = "_Email";
        public const string NameSession = "_Name";
        public const string UsernameSession = "_Username";

        [BindProperty]
        public User user { get; set; }

        [BindProperty(SupportsGet = true)]
        public string error { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AfzaSlaughter;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            string ReadQuery = "SELECT * FROM [AfzaSlaughter].[dbo].[User] where Email = '" + user.email + "'";
            SqlCommand ReadCommand = new SqlCommand(ReadQuery, con);
            con.Open();
            SqlDataReader reader = ReadCommand.ExecuteReader();
            if (reader.Read())//like if(reader.Read() == true)
            {
                error = "The user already has an account. Create a new account or login in!";
                return Page();
            }
            else
            {
                con.Close();
                string hash = BitConverter.ToString(MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(user.password))).Replace("-", "");
                string InsertQuery = "INSERT INTO [AfzaSlaughter].[dbo].[User] ([Username],[Email],[Password]) VALUES ('" + user.username.Trim() + "', '" + user.email.ToLower().Trim() + "', '" + hash + "')";
                SqlCommand InsertCommand = new SqlCommand(InsertQuery, con);
                con.Open();
                InsertCommand.ExecuteNonQuery();
                con.Close();
                HttpContext.Session.SetString(UsernameSession, user.username);
                HttpContext.Session.SetString(EmailSession, user.email);
                return RedirectToPage("/Profile", new { user.username, user.name, user.email });
            }
        }
    }
}
