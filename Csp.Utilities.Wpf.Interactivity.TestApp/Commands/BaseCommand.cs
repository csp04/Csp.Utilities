using System;
using System.ComponentModel;
using System.Windows.Input;
using PropertyChanged;

namespace Csp.Utilities.Wpf.Interactivity.TestApp.Commands
{
    [AddINotifyPropertyChangedInterface]
    public abstract class BaseCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public virtual bool CanExecute(object parameter) => true;

        public abstract void Execute(object parameter);
    }
}
