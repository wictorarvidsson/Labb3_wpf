using System;
using System.Collections.Generic;
using System.Text;
using DataAccesLayer.Models;
using DataAccesLayer.Interface;

namespace DataAccesLayer.Repositorys
{
    public class OldBookingRepository : GenericRepository<OldBooking>
    {
        public OldBookingRepository(MyContext context) : base(context)
        {

        }
    }
}
