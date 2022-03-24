using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace WPFApp.ViewModels.Commands
{
    internal class CommandHandler : ICommand
    {
        private Action _action;
        public event EventHandler CanExecuteChanged;

        //Takes Action parameter, method to be executed is written in the class that instantiates this class
        public CommandHandler(Action action)
        {
            _action = action;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _action();
        }
    }
}
