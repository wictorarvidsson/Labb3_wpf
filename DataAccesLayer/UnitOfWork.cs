using System;
using System.Collections.Generic;
using System.Text;
using DataAccesLayer.Interface;
using DataAccesLayer.Repositorys;
using System.Linq;

namespace DataAccesLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyContext context;

        public BookingRepository BookingRepository { get; set; }
        public ResidenceRepository ResidenceRepository { get; set; }
        public ReviewRepository ReviewRepository { get; set; }
        public UserRepository UserRepository { get; set; }
        public OldBookingRepository OldBookingRepository { get; set; }

        public UnitOfWork(MyContext context)
        {
            this.context = context;

            BookingRepository = new BookingRepository(context);
            ResidenceRepository = new ResidenceRepository(context);
            ReviewRepository = new ReviewRepository(context);
            UserRepository = new UserRepository(context);
            OldBookingRepository = new OldBookingRepository(context);
        }

        public void SaveChanges()
        {
            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
            }
        }
    }
}
