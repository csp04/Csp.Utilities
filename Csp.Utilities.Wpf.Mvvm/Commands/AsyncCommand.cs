using PropertyChanged;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Csp.Utilities.Wpf.Mvvm
{
    
    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync(object parameter);
        bool DisableWhenExecuting { get; set; }
        bool IsExecuting { get; set; }
    }

    [AddINotifyPropertyChangedInterface]
    public class AsyncCommand : BaseCommand, IAsyncCommand
    {
        
        private readonly Func<object, Task> _execute;
        private readonly Func<object, bool> _canExecute;

        public bool IsExecuting { get; set; } = false;
        public bool DisableWhenExecuting { get; set; } = false;

        public AsyncCommand(Func<object, Task> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public AsyncCommand(Func<Task> execute, Func<bool> canExecute = null)
        {
            _execute = _ => execute();
            _canExecute = _ => canExecute == null || canExecute();
        }

        public async Task ExecuteAsync(object parameter)
        {
            if (!IsExecuting)
            {
                try
                {
                    IsExecuting = true;
                    await _execute(parameter);
                }
                finally
                {
                    IsExecuting = false;
                }
            }

        }

        #region Explicit implementations
        public override bool CanExecute(object parameter)
        {
            if (_canExecute?.Invoke(parameter) ?? true)
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
            _ = ExecuteAsync(parameter);
        }
        #endregion
    }
}
