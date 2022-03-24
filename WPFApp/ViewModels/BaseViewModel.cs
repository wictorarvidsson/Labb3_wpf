using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPFApp.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        //Error message collection for each viewmodel
        public Dictionary<string, string> ErrorCollection { get; private set; } = new Dictionary<string, string>();

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        //Checks if ErrorCollection is empty before executing commands
        public bool InputErrorExists()
        {
            foreach (KeyValuePair<string, string> entry in ErrorCollection)
            {
                if (entry.Value != null)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
