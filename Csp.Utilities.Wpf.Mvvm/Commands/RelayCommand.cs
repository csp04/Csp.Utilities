using PropertyChanged;
using System;
using System.Threading.Tasks;

namespace Csp.Utilities.Wpf.Mvvm
{
    [AddINotifyPropertyChangedInterface]
    public class RelayCommand : BaseCommand
    {
        protected readonly Action<object> execute;
        private readonly Func<object, bool> canExecute;

        
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            this.execute = _ => execute();
            this.canExecute = _ => canExecute == null || canExecute();
        }
        public bool IsExecuting { get; protected set; } = false;
        public bool DisableWhenExecuting { get; set; } = false;

        public override bool CanExecute(object parameter)
        {
            if (canExecute?.Invoke(parameter) ?? true)
            {
                if (DisableWhenExecuting)
                {
                    return !IsExecuting;
                }
                else
                    return true;
            }

            return true;
        }

        public override void Execute(object parameter)
        {
            if (IsExecuting)
                return;

            IsExecuting = true;
            this.execute(parameter);
            IsExecuting = false;
        }
    }
}
