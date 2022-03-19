using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using BusinessLayer.Controllers;
using DataAccesLayer;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static MyContext MyContext = new MyContext();
        public static BookingController BookingController { get; } = new BookingController(MyContext);
        public static ResidenceController ResidenceController { get; } = new ResidenceController(MyContext);
        public static ReviewController ReviewController { get; } = new ReviewController(MyContext);
        public static UserController UserController { get; } = new UserController(MyContext);
    }
}
