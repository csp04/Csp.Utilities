using System;
using System.Windows;

namespace Csp.Utilities.Wpf.Mvvm
{
    public class WindowView<T> : Window, IView<T> where T : ViewModelBase
    {
        public T ViewModel
        {
            get => (T)DataContext;
            set
            {
                DataContext = value;
            }
        }

        public WindowView()
        {
            Loaded += WindowView_Loaded;

            ViewModel = IoC.Resolve<T>();
            Dispatcher.Invoke(async () => await ViewModel.Initialize());
        }

        private void WindowView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Unloaded += WindowView_Unloaded;
            Dispatcher.Invoke(async () => await ViewModel.OnAppearing());
        }

        private void WindowView_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Unloaded -= WindowView_Unloaded;
            Dispatcher.Invoke(async () => await ViewModel.OnDisappearing());
        }
    }
}
