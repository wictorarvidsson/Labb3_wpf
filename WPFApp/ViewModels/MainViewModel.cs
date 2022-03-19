using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using WPFApp.ViewModels.Commands;
using WPFApp.ViewModels;
using DataAccesLayer.Models;

namespace WPFApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private BaseViewModel _selectedViewModel;

        public Residence SelectedResidenceToEdit;
        public Residence selectedResidence;
        public User LoggedInUser;
        public BaseViewModel SelectedViewModel 
        {
            get { return _selectedViewModel; }
            set 
            {  
                _selectedViewModel = value; 
                OnPropertyChanged(nameof(SelectedViewModel));
            }
        }

        public MainViewModel()
        {
            _selectedViewModel = new LoginViewModel(this);
        }
    }
}
