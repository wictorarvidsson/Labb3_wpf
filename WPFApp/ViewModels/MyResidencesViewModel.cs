using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using DataAccesLayer.Models;
using WPFApp.ViewModels.Commands;

namespace WPFApp.ViewModels
{
    internal class MyResidencesViewModel : BaseViewModel
    {
        public int SelectedIndex { get; set; }
        public ICommand CreateAdCommand { get; set; }
        public List<Residence> MyResidences { get; set; }

        private ObservableCollection<string> _myResidenceString;
        public ObservableCollection<string> MyResidencesString
        {
            get
            {
                return _myResidenceString;
            }
            set
            {
                _myResidenceString = value;
                OnPropertyChanged(nameof(MyResidencesString));
            }
        }
        public List<string> MyResidenceBookings { get; set; }
        public ICommand BackToMenuCommand { get; set; }
        private MainViewModel mainViewModel;

        private ICommand _removeAdCommand;
        public ICommand RemoveAdCommand
        {
            get
            {
                return _removeAdCommand ?? (_removeAdCommand = new CommandHandler(() => RemoveAd()));
            }
        }

        private ICommand _editAdCommand;
        public ICommand EditAdCommand
        {
            get
            {
                return _editAdCommand ?? (_editAdCommand = new CommandHandler(() => EditAd()));
            }
        }
        public MyResidencesViewModel(MainViewModel mainViewModel)
        {
            BackToMenuCommand = new UpdateViewCommand(mainViewModel);
            CreateAdCommand = new UpdateViewCommand(mainViewModel);
            this.mainViewModel = mainViewModel;
            MyResidences = App.ResidenceController.GetByOwner(mainViewModel.LoggedInUser);
            MyResidencesString = new ObservableCollection<string>();
            MyResidenceBookings = new List<string>();

            foreach(Residence r in MyResidences)
            {
                MyResidencesString.Add("ID: " + r.ResidenceID + ". Adress: " + r.Street + ", " + r.Country);
            }

            foreach(Booking b in App.BookingController.ReturnBookingByResidenceOwner(mainViewModel.LoggedInUser))
            {
                MyResidenceBookings.Add("ID: " + b.BookingID + ". Totalpris: " + b.TotalPrice + ". By: " + b.User.Name);
            }
        }

        public void EditAd()
        {
            mainViewModel.SelectedResidenceToEdit = MyResidences[SelectedIndex];
            mainViewModel.SelectedViewModel = new EditAdViewModel(mainViewModel);
        }

        public void RemoveAd()
        {

            App.ResidenceController.RemoveResidence(MyResidences[SelectedIndex].ResidenceID, mainViewModel.LoggedInUser);
            MyResidencesString.RemoveAt(SelectedIndex);
        }
    }
}
