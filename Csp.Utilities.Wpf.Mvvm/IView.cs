namespace Csp.Utilities.Wpf.Mvvm
{
    public interface IView<T> where T : ViewModelBase
    {
        T ViewModel { get; set; }
    }
}
