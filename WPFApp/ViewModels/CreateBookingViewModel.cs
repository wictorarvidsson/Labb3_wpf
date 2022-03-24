using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using WPFApp.ViewModels.Commands;
using System.Diagnostics;


namespace WPFApp.ViewModels
{
    class CreateBookingViewModel : BaseViewModel
    {
        private string _reservedString;
        
        //Used to display if residence is selected during selected dates
        public string ReservedString
        {
            get
            {
                return _reservedString;
            }
            set
            {
                _reservedString = value;
                OnPropertyChanged(nameof(ReservedString));
            }
        }

        private string _totalPrice;
        public string TotalPrice
        {
            get
            {
                return _totalPrice;
            }
            set
            {
                _totalPrice = value;
                OnPropertyChanged(nameof(TotalPrice));
            }
        }

        public string ImageUrl { get; set; }
        public string NumberOfGuests { get; set; }

        private DateTime _selectedFromDate;
        public DateTime SelectedFromDate
        {
            get
            {
                return _selectedFromDate;
            }
            set
            {
                _selectedFromDate = value;
                //Error handling for invalid date selection, cant select before today
                if (SelectedFromDate < DateTime.Today || (SelectedToDate - SelectedFromDate).TotalDays < 0)
                {
                    TotalPrice = "Invalid selection";
                }
                else
                {
                    TotalPrice = ((SelectedToDate - SelectedFromDate).TotalDays * mainViewModel.selectedResidence.PricePerDay).ToString() + "kr";
                }
            }
        }

        private DateTime _selectedToDate;
        public DateTime SelectedToDate
        {
            get
            {
                return _selectedToDate;
            }
            set
            {
                _selectedToDate = value;
                //Error handling for invalid date selection, cant select before today or have a negative amount of days selected
                if ((SelectedToDate - SelectedFromDate).TotalDays < 0 || SelectedFromDate < DateTime.Today)
                {
                    TotalPrice = "Invalid selection";
                }

                else
                {
                    TotalPrice = ((SelectedToDate - SelectedFromDate).TotalDays * mainViewModel.selectedResidence.PricePerDay).ToString() + "kr";
                }
            }
        }

        public ICommand BackToMenuCommand { get; set; }
        private ICommand _createBookingCommand;
        private MainViewModel mainViewModel;
        public ICommand CreateBookingCommand
        {
            get
            {
                return _createBookingCommand ?? (_createBookingCommand = new CommandHandler(() => CreateBooking()));
            }
        }

        public CreateBookingViewModel(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
            SelectedFromDate = DateTime.Today;
            SelectedToDate = DateTime.Today;
            BackToMenuCommand = new UpdateViewCommand(mainViewModel);
            ImageUrl = mainViewModel.selectedResidence.ImageUrl;
        }

        public void CreateBooking()
        {
            if (!CheckIfReserved() && TotalPrice != "Invalid selection")
            {
                int NumberOfGuestsint = Convert.ToInt32(NumberOfGuests);
                App.BookingController.NewBooking(mainViewModel.selectedResidence, mainViewModel.LoggedInUser, SelectedFromDate, SelectedToDate, NumberOfGuestsint);
                mainViewModel.SelectedViewModel = new MenuViewModel(mainViewModel);
            }
        }

        //Checks if selected residence is reserved during selected dates
        public bool CheckIfReserved()
        {
            foreach(DateTime d in App.BookingController.ReturnBookedDays(mainViewModel.selectedResidence))
            {
                if (d >= SelectedFromDate && d <= SelectedToDate)
                {
                    ReservedString = "Residence is reserved during those dates";
                    return true;
                }
            }
            return false;
        }
    }
}
