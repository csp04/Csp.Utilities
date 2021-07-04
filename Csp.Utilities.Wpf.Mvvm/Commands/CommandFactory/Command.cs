using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Csp.Utilities.Wpf.Mvvm
{
    /// <summary>
    /// Command factory.
    /// </summary>
    public static class Command
    {
        /// <summary>
        /// Creates command.
        /// </summary>
        /// <param name="execute"></param>
        /// <param name="canExecute"></param>
        /// <returns></returns>
        public static ICommand Create(Action<object> execute, Func<object, bool> canExecute = null) => new RelayCommand(execute, canExecute);

        /// <summary>
        /// Creates command.
        /// </summary>
        /// <param name="execute"></param>
        /// <param name="canExecute"></param>
        /// <returns></returns>
        public static ICommand Create(Action execute, Func<bool> canExecute = null) => new RelayCommand(execute, canExecute);

        /// <summary>
        /// Creates async command.
        /// </summary>
        /// <param name="execute"></param>
        /// <param name="canExecute"></param>
        /// <returns></returns>
        public static IAsyncCommand CreateAsync(Func<object, Task> execute, Func<object, bool> canExecute = default, Action<IAsyncCommand> option = default) 
        {
            var cmd = new AsyncCommand(execute, canExecute);
            option?.Invoke(cmd);
            return cmd;
        } 

        /// <summary>
        /// Creates async command.
        /// </summary>
        /// <param name="execute"></param>
        /// <param name="canExecute"></param>
        /// <returns></returns>
        public static IAsyncCommand CreateAsync(Func<Task> execute, Func<bool> canExecute = default, Action<IAsyncCommand> option = default)
        {
            var cmd = new AsyncCommand(execute, canExecute);
            option?.Invoke(cmd);
            return cmd;
        }

        /// <summary>
        /// Creates an empty command.
        /// </summary>
        public static ICommand Empty = Create(() => { });


    }
}
