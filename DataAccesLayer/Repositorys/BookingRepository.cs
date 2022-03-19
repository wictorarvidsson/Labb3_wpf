using System;
using System.Collections.Generic;
using System.Text;
using DataAccesLayer.Models;
using DataAccesLayer.Interface;

namespace DataAccesLayer.Repositorys
{
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
    {
        public BookingRepository(MyContext context) : base(context)
        {

        }
    }
}
