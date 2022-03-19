using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccesLayer.Models
{
    public class Review
    {
        public int ReviewID { get; set; }
        public string Text { get; set; }
        public User User { get; set; }
    }
}
