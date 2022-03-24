using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using WPFApp.ViewModels;
using WPFApp.ViewModels.Commands;

namespace WPFApp.ViewModels
{
    internal class RegisterViewModel : BaseViewModel , IDataErrorInfo
    {
        public MainViewModel mainViewModel;
        public string Username { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public ICommand BackToMenuCommand { get; set; }
        private ICommand _registerCommand;
        public ICommand RegisterCommand
        {
            get
            {
                return _registerCommand ?? (_registerCommand = new CommandHandler(() => RegisterUser()));
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
                    case "Username":
                        if (!string.IsNullOrEmpty(Username) && App.UserController.CheckIfUsernameExists(Username))
                        {
                            result = "Username already exists";
                        }
                        break;
                    case "Password":
                        if (!string.IsNullOrEmpty(Password) && Password.Length < 6)
                        {
                            result = "Password needs to be atleast 6 characters long";
                        }
                        break;
                    case "PhoneNumber":
                        if (!int.TryParse(PhoneNumber, out _))
                        {
                            result = "Phonenumber needs to be a number";
                        }
                        break;
                    case "Email":
                        if (!string.IsNullOrEmpty(Email) && !Email.Contains("@"))
                        {
                            result = "Invalid email";
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

        public void RegisterUser()
        {
            if (!InputErrorExists())
            {
                App.UserController.RegisterNewUser(Username, Name, Password, Email, Convert.ToInt32(PhoneNumber));
                mainViewModel.SelectedViewModel = new LoginViewModel(mainViewModel);
            }
        }

        public RegisterViewModel(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
            BackToMenuCommand = new UpdateViewCommand(mainViewModel);
        }
    }
}
