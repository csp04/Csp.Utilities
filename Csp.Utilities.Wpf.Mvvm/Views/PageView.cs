using System;
using System.Windows.Controls;

namespace Csp.Utilities.Wpf.Mvvm
{
    public class PageView<T> : Page, IView<T> where T : ViewModelBase
    {
        public T ViewModel 
        { 
            get => (T)DataContext;
            set
            {
                DataContext = value;
            }
        }

        public PageView()
        {
            Loaded += PageView_Loaded;

            ViewModel = IoC.Resolve<T>();
            Dispatcher.Invoke(async () => await ViewModel.Initialize());
        }

        private void PageView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Unloaded += PageView_Unloaded;
            Dispatcher.Invoke(async () => await ViewModel.OnAppearing());
        }

        private void PageView_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Unloaded -= PageView_Unloaded;
            Dispatcher.Invoke(async () => await ViewModel.OnDisappearing());
        }
    }
}
