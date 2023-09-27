using System;
using Microsoft.EntityFrameworkCore;
using DataAccesLayer.Models;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace DataAccesLayer
{
    public class MyContext : DbContext
    {

        // Project used a database provided by school which is no longer in use
        private string connectionString = "";
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Residence> Residences { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OldBooking> OldBooking { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
