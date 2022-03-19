using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccesLayer.Models
{
    public class User : Account
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
