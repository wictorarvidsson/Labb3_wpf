using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccesLayer.Models
{
    public class Booking
    {
        public int BookingID { get; set; }
        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }
        public int NumberOfGuests { get; set; }
        public double TotalPrice { get; set; }
        public Residence Residence { get; set; }
        public User User { get; set; }
    }
}
