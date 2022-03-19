using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using DataAccesLayer.Models;
using WPFApp.ViewModels.Commands;
using System.Diagnostics;

namespace WPFApp.ViewModels
{
    internal class MyBookingsViewModel : BaseViewModel
    {
        public ICommand BackToMenuCommand { get; set; }
        public int SelectedIndex { get; set; }
        public List<Booking> MyBookings { get; set; }
        public List<string> MyBookingsString { get; set; }
        public List<OldBooking> myOldBookings = new List<OldBooking>();
        public MainViewModel mainViewModel;
        private ICommand _cancelBookingCommand;
        public ICommand CancelBookingCommand
        {
            get
            {
                return _cancelBookingCommand ?? (_cancelBookingCommand = new CommandHandler(() => CancelBooking()));
            }
        }
        public MyBookingsViewModel(MainViewModel mainViewModel)
        {
            BackToMenuCommand = new UpdateViewCommand(mainViewModel);

            this.mainViewModel = mainViewModel;
            MyBookings = App.BookingController.ReturnBookingsByUser(mainViewModel.LoggedInUser);

            MyBookingsString = new List<string>();
            foreach(Booking booking in MyBookings)
            {
                MyBookingsString.Add("ID: " + booking.BookingID + ". Total pris: " + booking.TotalPrice + ".");
            }
        }

        public void CancelBooking()
        {
            Trace.WriteLine(SelectedIndex);
            Trace.WriteLine(MyBookings[SelectedIndex].BookingID);
            Trace.WriteLine(mainViewModel.LoggedInUser.Username);
            App.BookingController.RemoveBookingByUser(MyBookings[SelectedIndex].BookingID, mainViewModel.LoggedInUser);
        }
    }
}
