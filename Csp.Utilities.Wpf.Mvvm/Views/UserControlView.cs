using System;
using System.Windows.Controls;

namespace Csp.Utilities.Wpf.Mvvm
{
    public class UserControlView<T> : UserControl, IView<T> where T : ViewModelBase
    {
        public T ViewModel
        {
            get => (T)DataContext;
            set
            {
                DataContext = value;
            }
        }

        public UserControlView()
        {
            Loaded += UserControlView_Loaded;

            ViewModel = IoC.Resolve<T>();
            Dispatcher.Invoke(async () => await ViewModel.Initialize());
        }

        private void UserControlView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Unloaded += UserControlView_Unloaded;
            Dispatcher.Invoke(async () => await ViewModel.OnAppearing());
        }

        private void UserControlView_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Unloaded -= UserControlView_Unloaded;
            Dispatcher.Invoke(async () => await ViewModel.OnDisappearing());
        }
    }
}
