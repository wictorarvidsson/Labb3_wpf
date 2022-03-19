using System;
using System.Collections.Generic;
using System.Text;
using DataAccesLayer.Models;
using BusinessLayer.Controllers;
using System.Windows.Input;
using WPFApp.ViewModels.Commands;
using System.Linq;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace WPFApp.ViewModels
{
    public class BookResidenceViewModel : BaseViewModel
    {
        public int SelectedIndex { get; set; }

        public List<Residence> Residences { get; set; }

        private ObservableCollection<string> residencesString;
        public ObservableCollection<string> ResidencesString
        {
            get
            {
                return residencesString;
            }
            set
            {
                residencesString = value;
                OnPropertyChanged(nameof(ResidencesString));
            }
        }

        public bool Balcony { get; set; }
        public bool Kitchen { get; set; }
        public bool Wifi { get; set; }
        public bool Pool { get; set; }
        public double PricePerDay { get; set; }
        public bool price0to100 { get; set; }
        public bool price100to200 { get; set; }
        public bool price200to300 { get; set; }
        public bool price300up { get; set; }
        public bool priceAny { get; set; }
        public double Rating { get; set; }
        public string CitySearch { get; set; }
        public ICommand BackToMenuCommand { get; set; }

        private ICommand _sortCommand;
        public ICommand SortCommand
        {
            get
            {
                return _sortCommand ?? (_sortCommand = new CommandHandler(() => SortResidenceList()));
            }
        }
        private ICommand _createBookingCommand;
        public ICommand CreateBookingCommand
        {
            get
            {
                return _createBookingCommand ?? (_createBookingCommand = new CommandHandler(() => CreateBooking()));
            }
        }

        private MainViewModel mainViewModel;

        public BookResidenceViewModel(MainViewModel mainViewModel)
        {
            BackToMenuCommand = new UpdateViewCommand(mainViewModel);
            this.mainViewModel = mainViewModel;
            Residences = App.ResidenceController.GetAll();
            residencesString = new ObservableCollection<string>();
            CreateResidenceString();
            CitySearch = "";
        }

        public void CreateResidenceString()
        {
            residencesString.Clear();
            foreach (Residence r in Residences)
            {
                residencesString.Add("Adress: " + r.Street + ", " + r.City + ", Price/day: " + r.PricePerDay + ", Rating: " + r.Rating);
            }

            if (residencesString.Count == 0)
            {
                residencesString.Add("No matches found for your search.");
            }
        }

        public void CreateBooking()
        {
            mainViewModel.selectedResidence = Residences[SelectedIndex];
            mainViewModel.SelectedViewModel = new CreateBookingViewModel(mainViewModel);
        }

        public void SortResidenceList()
        {

            Residences = App.ResidenceController.GetByFacilites(Balcony, Kitchen, Wifi, Pool);
            if (CitySearch.ToCharArray().Length > 0)
            {
                Residences = Residences.Where(r => r.City.Contains(CitySearch)).ToList();
            }

            if (price0to100)
            {
                Residences = Residences.Where(r => r.PricePerDay <= 100).ToList();
            }
            else if (price100to200)
            {
                Residences = Residences.Where(r => r.PricePerDay >= 100 && r.PricePerDay <= 200).ToList();
            }
            else if (price200to300)
            {
                Residences = Residences.Where(r => r.PricePerDay >= 200 && r.PricePerDay <= 300).ToList();
            }
            else if (price300up)
            {
                Residences = Residences.Where(r => r.PricePerDay >= 300).ToList();
            }

            CreateResidenceString();
            //OnPropertyChanged(nameof(ResidencesString));
            Trace.WriteLine("sorted");
        }
    }
}
