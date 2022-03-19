using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccesLayer.Models
{
    public class Residence
    {
        public Residence()
        {
            Rating = 0;
        }

        public User Owner { get; set; }
        public int ResidenceID { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int PostalCode { get; set; }
        public string Country { get; set; }
        public bool Balcony { get; set; }
        public bool Kitchen { get; set; }
        public bool Wifi { get; set; }
        public bool Pool { get; set; }
        public int MaxGuests { get; set; }
        public double PricePerDay { get; set; }
        public double Rating { get; set; }
        public string Description { get; set; }
        public List<Review> Reviews { get; set; }

        //IMAGE
    }
}
