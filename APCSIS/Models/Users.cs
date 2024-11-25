using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static APCSIS.Includes.PublicVariables;

namespace APCSIS.Models
{
    public class Users
    {
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Mname { get; set; }
        public string email { get; set; }
        public string password { get; set; }


        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public async Task<bool> AddUser(string f_name, string l_name, string m_name, string emailadd, string userpass)
        { 
            try
            {
                string hashedPassword = HashPassword(userpass);
                Users user = new Users()
                {
                    Fname = f_name,
                    Lname = l_name,
                    Mname = m_name,
                    email = emailadd,
                    password = userpass
                };
                await client
                    .Child("Users")
                    .PostAsync(user);
                client.Dispose();
                return true;
            }
            catch
            {
                return false;
            }
        }


        //public async Task<bool> Login(string emailadd, string password)
        //{
        //    try
        //    {
        //        // Retrieve all users from Firebase
        //        var users = await client
        //            .Child("Users")
        //            .OnceAsync<Users>();

        //        // Check if the email exists in the database
        //        var user = users.FirstOrDefault(u => u.Object.email == emailadd);
        //        if (user != null)
        //        {
        //            // Hash the input password
        //            string hashedPassword = HashPassword(password);

        //            // Compare the hashed password
        //            if (user.Object.password == hashedPassword)
        //            {
        //                return true; // Login successful
        //            }
        //        }
        //        return false; // Login failed, either email doesn't exist or password is incorrect
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle any errors, e.g., Firebase connection failure
        //        Console.WriteLine($"Error logging in: {ex.Message}");
        //        return false;
        //    }
        //}
        public async Task<bool> EditUser(string f_name, string l_name, string m_name, string emailadd, string userpass)
        {
            try
            {
                Users user = new Users()
                {
                    Fname = f_name,
                    Lname = l_name,
                    Mname = m_name,
                    email = emailadd,
                    password = userpass
                };
                await client
                    .Child("Users")
                    .PatchAsync(user);
                client.Dispose();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<string> Login(string mail, string _password)
        {
            try
            {
                var evaluateEmail = (await client
                    .Child("Users")
                    .OnceAsync<Users>()).FirstOrDefault
                    (i => i.Object.email == mail && i.Object.password == _password);
                if (evaluateEmail == null) return null;
                return evaluateEmail.Key;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
