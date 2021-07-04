using Csp.Utilities.Wpf.Interactivity.TestApp.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace Csp.Utilities.Wpf.Interactivity.TestApp.ViewModels
{
    public class ListViewModel
    {
        public ObservableCollection<string> Lists { get; set; } = new ObservableCollection<string>();

        public ListViewModel()
        {
            
        }
        public ICommand RefreshCommand => Command.Create(() => Refresh());

        public void Refresh()
        {
            Lists.Clear();
            for (int i = 0; i < 10000; i++)
            {
                Lists.Add("Item " + (i + 1));
            }
        }
    }
}
