using System;
using System.Windows.Input;

namespace Csp.Utilities.Wpf.Interactivity.TestApp.Commands
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
        /// Creates an empty command.
        /// </summary>
        public static ICommand Empty = Create(() => { });


    }
}
