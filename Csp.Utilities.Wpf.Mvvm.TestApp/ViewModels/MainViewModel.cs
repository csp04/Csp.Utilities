using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Csp.Utilities.Wpf.Mvvm.TestApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public string StatusDisplay { get; set; }

        public override async Task Initialize()
        {
            StatusDisplay = "Loading";
            await Task.Delay(2000);
            StatusDisplay = "Loaded";
        }

        public override Task OnAppearing()
        {
            return base.OnAppearing();
        }

        public ICommand NameCommand => Command.CreateAsync(NameAsync, option: cmd => cmd.DisableWhenExecuting = true);

        public async Task NameAsync()
        {
            StatusDisplay = "Loading";
            await Task.Delay(2000);
            StatusDisplay = "Loaded";
            MessageBox.Show("Delayed 2000");
        }
    }
}
