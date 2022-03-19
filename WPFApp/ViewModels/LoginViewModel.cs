using DataAccesLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using WPFApp.ViewModels.Commands;
using DataAccesLayer.Models;

namespace WPFApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public MainViewModel mainViewModel;
        public string Username { get; set; }
        public string Password { get; set; }

        private string _loginErrorUsername;
        public string LoginErrorUsername
        {
            get
            {
                return _loginErrorUsername;
            }
            set
            {
                _loginErrorUsername = value;
                OnPropertyChanged(nameof(LoginErrorUsername));
            }
        }

        private string _loginErrorPassword;
        public string LoginErrorPassword
        {
            get
            {
                return _loginErrorPassword;
            }
            set
            {
                _loginErrorPassword = value;
                OnPropertyChanged(nameof(LoginErrorPassword));
            }
        }

        private ICommand _loginCommand;
        public ICommand LogInCommand 
        { 
            get
            {
                return _loginCommand ?? (_loginCommand = new CommandHandler(() => LogIn()));
            }
        }
        public ICommand UpdateViewCommand { get; set; }

        public LoginViewModel(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
            UpdateViewCommand = new UpdateViewCommand(mainViewModel);
        }

        public void LogIn()
        {
            LoginErrorPassword = null;
            LoginErrorUsername = null;
            User user = App.UserController.GetByUserName(Username);
            if (user != null && App.UserController.LogIn(user, Password))
            {
                mainViewModel.LoggedInUser = user;

                //öppna nytt fönster
                mainViewModel.SelectedViewModel = new MenuViewModel(mainViewModel);
            }
            else if (user != null)
            {
                LoginErrorPassword = "Wrong password";
            }
            else
            {
                LoginErrorUsername = "Username does not exist";
            }
        }
    }
}
