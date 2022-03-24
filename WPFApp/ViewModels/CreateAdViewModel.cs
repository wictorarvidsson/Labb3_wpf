using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using WPFApp.ViewModels.Commands;

namespace WPFApp.ViewModels
{
    class CreateAdViewModel : BaseViewModel , IDataErrorInfo
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string MaxGuests { get; set; }
        public string PricePerDay { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool Balcony { get; set; }
        public bool Kitchen { get; set; }
        public bool Wifi { get; set; }
        public bool Pool { get; set; }
        public ICommand BackToMenuCommand { get; set; }

        private ICommand _createAdCommand;
        private MainViewModel mainViewModel;
        public ICommand CreateAdCommand
        {
            get
            {
                return _createAdCommand ?? (_createAdCommand = new CommandHandler(() => CreateAd()));
            }
        }

        public string Error => throw new NotImplementedException();

        //Sets data error info based on user input
        public string this[string name]
        {
            get
            {
                string result = null;

                switch (name)
                {
                    case "PostalCode":
                        if (!int.TryParse(PostalCode, out _))
                        {
                            result = "Postal code needs to be a number";
                        }
                        break;
                    case "MaxGuests":
                        if (!int.TryParse(MaxGuests, out _))
                        {
                            result = "Max guests needs to be a number";
                        }
                        break;
                    case "PricePerDay":
                        if (!int.TryParse(MaxGuests, out _))
                        {
                            result = "Price per day needs to be a number";
                        }
                        break;
                    case "ImageUrl":
                        if (ImageUrl != null && !IsImageUrl())
                        {
                            result = "Image link needs to end with .png, .jpg or .jpeg";
                        }
                        break;
                }

                if (ErrorCollection.ContainsKey(name))
                {
                    ErrorCollection[name] = result;
                }
                else if (result != null)
                {
                    ErrorCollection.Add(name, result);
                }
                OnPropertyChanged(nameof(ErrorCollection));
                return result;
            }
        }

        public CreateAdViewModel(MainViewModel mainViewModel)
        {
            BackToMenuCommand = new UpdateViewCommand(mainViewModel);
            this.mainViewModel = mainViewModel;
        }

        public void CreateAd()
        {
            //button works if ErrorCollection doesnt contain any strings
            if (!InputErrorExists())
            {
                App.ResidenceController.AddNewResidence(mainViewModel.LoggedInUser, Street, City, State, Convert.ToInt32(PostalCode), Country, Balcony, Kitchen, Wifi, Pool, Convert.ToInt32(MaxGuests), Convert.ToDouble(PricePerDay), Description, ImageUrl);
                mainViewModel.SelectedViewModel = new MenuViewModel(mainViewModel);
            }
        }

        //Checks if URL input ends with .png, .jpg or .jpeg
        private bool IsImageUrl()
        {
            if (ImageUrl.Length >= 4)
            {
                if (ImageUrl.Substring(ImageUrl.Length - 4).ToLower() == ".png" || ImageUrl.Substring(ImageUrl.Length - 4).ToLower() == ".jpg" || ImageUrl.Substring(ImageUrl.Length - 4).ToLower() == "jpeg")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }
}
