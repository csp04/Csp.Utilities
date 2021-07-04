using System;

namespace Csp.Utilities.Wpf.Interactivity.TestApp.Commands 
{
    public class RelayCommand : BaseCommand
    {
        private readonly Action<object> execute;
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

        public override bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute(parameter);
        }

        public override void Execute(object parameter)
        {
            IsExecuting = true;
            this.execute(parameter);
            IsExecuting = false;
        }
    }
}
