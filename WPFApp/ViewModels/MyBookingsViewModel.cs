using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using DataAccesLayer.Models;
using WPFApp.ViewModels.Commands;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace WPFApp.ViewModels
{
    internal class MyBookingsViewModel : BaseViewModel
    {
        public ICommand BackToMenuCommand { get; set; }
        public int SelectedIndex { get; set; }
        public List<Booking> MyBookings { get; set; }

        private ObservableCollection<string> _myBookingsString;
        public ObservableCollection<string> MyBookingsString
        {
            get
            {
                return _myBookingsString;
            }
            set
            {
                _myBookingsString = value;
                OnPropertyChanged(nameof(MyBookingsString));
            }
        }
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

            MyBookingsString = new ObservableCollection<string>();
            foreach(Booking booking in MyBookings)
            {
                MyBookingsString.Add("ID: " + booking.BookingID + ". Total pris: " + booking.TotalPrice + ".");
            }
        }

        public void CancelBooking()
        {
            if (App.BookingController.RemoveBookingByUser(MyBookings[SelectedIndex].BookingID, mainViewModel.LoggedInUser))
            {
                MyBookingsString.RemoveAt(SelectedIndex);
            }
        }
    }
}
