using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using WPFApp.ViewModels;

namespace WPFApp.ViewModels.Commands
{
    public class UpdateViewCommand : ICommand
    {
        
        private MainViewModel mainViewModel;

        public UpdateViewCommand(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        //Sets SelectedViewModel based on CommandParameter from xaml view
        public void Execute(object parameter)
        {
            if (parameter.ToString() == "BookResidence")
            {
                mainViewModel.SelectedViewModel = new BookResidenceViewModel(mainViewModel);
            }
            else if (parameter.ToString() == "MyBookings")
            {
                mainViewModel.SelectedViewModel = new MyBookingsViewModel(mainViewModel);
            }
            else if (parameter.ToString() == "MyResidences")
            {
                mainViewModel.SelectedViewModel = new MyResidencesViewModel(mainViewModel);
            }
            else if (parameter.ToString() == "RegisterUser")
            {
                mainViewModel.SelectedViewModel = new RegisterViewModel(mainViewModel);
            }
            else if (parameter.ToString() == "CreateAd")
            {
                mainViewModel.SelectedViewModel = new CreateAdViewModel(mainViewModel);
            }
            else if (parameter.ToString() == "EditAd")
            {
                mainViewModel.SelectedViewModel = new EditAdViewModel(mainViewModel);
            }
            else if (parameter.ToString() == "BackToMenu")
            {
                mainViewModel.SelectedViewModel = new MenuViewModel(mainViewModel);
            }
            else if (parameter.ToString() == "BackToLogin")
            {
                mainViewModel.SelectedViewModel = new LoginViewModel(mainViewModel);
            }
        }      
    }
}

       