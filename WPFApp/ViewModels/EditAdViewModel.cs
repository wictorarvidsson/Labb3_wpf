using System;
using System.Collections.Generic;
using System.Text;
using WPFApp.ViewModels.Commands;
using System.Windows.Input;
using DataAccesLayer.Models;

namespace WPFApp.ViewModels
{
    public class EditAdViewModel : BaseViewModel
    {
        private MainViewModel mainViewModel;

        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string MaxGuests { get; set; }
        public string PricePerDay { get; set; }
        public string Description { get; set; }
        public bool Balcony { get; set; }
        public bool Kitchen { get; set; }
        public bool Wifi { get; set; }
        public bool Pool { get; set; }
        public ICommand BackToMenuCommand { get; set; }

        private ICommand _saveChangesCommand;
        public ICommand SaveChangesCommand
        {
            get
            {
                return _saveChangesCommand ?? (_saveChangesCommand = new CommandHandler(() => SaveChanges()));
            }
        }

        public EditAdViewModel(MainViewModel mainViewModel)
        {
            BackToMenuCommand = new UpdateViewCommand(mainViewModel);
            this.mainViewModel = mainViewModel;

            Street = mainViewModel.SelectedResidenceToEdit.Street;
            City = mainViewModel.SelectedResidenceToEdit.City;
            State = mainViewModel.SelectedResidenceToEdit.State;
            PostalCode = mainViewModel.SelectedResidenceToEdit.PostalCode.ToString();
            Country = mainViewModel.SelectedResidenceToEdit.Country;
            MaxGuests = mainViewModel.SelectedResidenceToEdit.MaxGuests.ToString();
            PricePerDay = mainViewModel.SelectedResidenceToEdit.PricePerDay.ToString();
            Description = mainViewModel.SelectedResidenceToEdit.Description;
            Pool = mainViewModel.SelectedResidenceToEdit.Pool;
            Balcony = mainViewModel.SelectedResidenceToEdit.Balcony;
            Wifi = mainViewModel.SelectedResidenceToEdit.Wifi;
            Kitchen = mainViewModel.SelectedResidenceToEdit.Kitchen;
        }

        public void SaveChanges()
        {
            Residence newResidence = new Residence()
            {
                Owner = mainViewModel.LoggedInUser,
                Street = Street,
                City = City,
                State = State,
                PostalCode = Convert.ToInt32(PostalCode),
                Country = Country,
                Balcony = Balcony,
                Kitchen = Kitchen,
                Wifi = Wifi,
                Pool = Pool,
                MaxGuests = Convert.ToInt32(MaxGuests),
                PricePerDay = Convert.ToDouble(PricePerDay),
                Description = Description
            };

            App.ResidenceController.EditResidence(mainViewModel.SelectedResidenceToEdit, newResidence);
            mainViewModel.SelectedViewModel = new MyResidencesViewModel(mainViewModel);
        }
    }
}
