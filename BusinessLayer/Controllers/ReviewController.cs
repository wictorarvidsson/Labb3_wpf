using System;
using System.Collections.Generic;
using System.Text;
using DataAccesLayer.Repositorys;
using System.Linq;

namespace BusinessLayer.Controllers
{
    public class ReviewController
    {
        private DataAccesLayer.UnitOfWork unitOfWork;

        public ReviewController(DataAccesLayer.MyContext context)
        {
            unitOfWork = new DataAccesLayer.UnitOfWork(context);
        }

        public List<DataAccesLayer.Models.OldBooking> GetOldBookings(DataAccesLayer.Models.User user)
        {
            return unitOfWork.OldBookingRepository.Find(o => o.Booking.User == user).ToList();
        }
        //Add new review
        public void AddNewReview(DataAccesLayer.Models.User user, DataAccesLayer.Models.OldBooking oldBooking, string review)
        {
            //if ()
        }
    }
}
