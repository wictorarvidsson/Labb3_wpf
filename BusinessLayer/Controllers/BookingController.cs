using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DataAccesLayer.Repositorys;


namespace BusinessLayer.Controllers
{
    public class BookingController
    {
        //private BookingRepository bookingRepository;
        private DataAccesLayer.UnitOfWork unitOfWork;

        public BookingController(DataAccesLayer.MyContext context)
        {
            unitOfWork = new DataAccesLayer.UnitOfWork(context);
        }

        //return bookings on residence by owner
        public List<DataAccesLayer.Models.Booking> ReturnBookingByResidenceOwner(DataAccesLayer.Models.User user)
        {
            return unitOfWork.BookingRepository.Find(b => b.Residence != null && b.Residence.Owner == user).ToList();
        }

        //return bookings owned by user
        public List<DataAccesLayer.Models.Booking> ReturnBookingsByUser(DataAccesLayer.Models.User user)
        {
            return unitOfWork.BookingRepository.Find(b => b.User == user).ToList();
        }

        //return days where the residence is booked
        public List<DateTime> ReturnBookedDays(DataAccesLayer.Models.Residence residence)
        {
            List<DateTime> unavailableDates = new List<DateTime>();

            //show available days based on bookings
            List<DataAccesLayer.Models.Booking> matchingBookings = unitOfWork.BookingRepository.Find(b => b.Residence == residence).ToList();

            //append unavailbeDates list by days between fromTime, toTime for each booking
            foreach(DataAccesLayer.Models.Booking bookings in matchingBookings)
            {
                unavailableDates.AddRange(Enumerable.Range(0, 1 + bookings.ToTime.Subtract(bookings.FromTime).Days).Select(offset => bookings.FromTime.AddDays(offset)).ToArray());
            }
            return unavailableDates;
        }

        //add new booking
        public void NewBooking(DataAccesLayer.Models.Residence residence, DataAccesLayer.Models.User user, DateTime fromTime, DateTime toTime, int numberOfGuests)
        {
            DataAccesLayer.Models.Booking newBooking = new DataAccesLayer.Models.Booking()
            {
                Residence = residence,
                User = user,
                FromTime = fromTime,
                ToTime = toTime,
                NumberOfGuests = numberOfGuests,
                TotalPrice = (toTime - fromTime).TotalDays * residence.PricePerDay
            };
            unitOfWork.BookingRepository.Add(newBooking);
            unitOfWork.SaveChanges();
        }

        //remove booking by owner of residence
        public void RemoveBookingByHost(int bookingId, DataAccesLayer.Models.User user)
        {
            //check if owner
            DataAccesLayer.Models.Booking booking = unitOfWork.BookingRepository.FirstOrDefault(b => b.BookingID == bookingId);

            if (booking.Residence.Owner == user)
            {
                unitOfWork.OldBookingRepository.Add(new DataAccesLayer.Models.OldBooking() {Booking = booking});
                unitOfWork.BookingRepository.Remove(booking);
                unitOfWork.SaveChanges();
            }
        }

        //remove booking by user that booked the residence
        public bool RemoveBookingByUser(int bookingId, DataAccesLayer.Models.User user)
        {
            //check date
            DataAccesLayer.Models.Booking booking = unitOfWork.BookingRepository.FirstOrDefault(b => b.BookingID == bookingId);

            if (DateTime.Now < booking.FromTime && booking.User == user)
            {
                unitOfWork.OldBookingRepository.Add(new DataAccesLayer.Models.OldBooking() { Booking = booking });
                unitOfWork.BookingRepository.Remove(booking);
                unitOfWork.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
