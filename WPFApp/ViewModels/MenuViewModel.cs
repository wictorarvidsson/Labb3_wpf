using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using WPFApp.ViewModels.Commands;

namespace WPFApp.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        private MainViewModel mainViewModel;
        public ICommand SelectOperation { get; set; }
        private ICommand _logoutCommand;
        public ICommand LogoutCommand
        {
            get
            {
                return _logoutCommand ?? (_logoutCommand = new CommandHandler(() => Logout()));
            }
        }
        public MenuViewModel(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
            SelectOperation = new UpdateViewCommand(mainViewModel);
        }

        public void Logout()
        {
            mainViewModel.LoggedInUser = null;
            mainViewModel.SelectedViewModel = new LoginViewModel(mainViewModel);
        }
    }
}
