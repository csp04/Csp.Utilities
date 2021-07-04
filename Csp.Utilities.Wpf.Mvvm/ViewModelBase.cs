using PropertyChanged;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Csp.Utilities.Wpf.Mvvm
{
    [AddINotifyPropertyChangedInterface]
    public abstract class ViewModelBase
    {
        public virtual Task Initialize() => Task.CompletedTask;

        public virtual Task OnAppearing() => Task.CompletedTask;

        public virtual Task OnDisappearing() => Task.CompletedTask;
    }
}
