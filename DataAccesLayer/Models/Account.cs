using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccesLayer.Models
{
    public class Account
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public bool VerifyPassword(string password)
        {
            return (password == this.Password);
        }
    }
}
