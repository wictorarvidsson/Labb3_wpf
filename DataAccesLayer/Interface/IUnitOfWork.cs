using System;
using System.Collections.Generic;
using System.Text;
using DataAccesLayer.Models;
using DataAccesLayer.Repositorys;
using DataAccesLayer.Interface;

namespace DataAccesLayer
{
    public interface IUnitOfWork
    {
        BookingRepository BookingRepository { get; set; }
        ResidenceRepository ResidenceRepository { get; set; }
        ReviewRepository ReviewRepository { get; set; }
        UserRepository UserRepository { get; set; }
        OldBookingRepository OldBookingRepository { get; set; }

        public void SaveChanges();

    }
}
