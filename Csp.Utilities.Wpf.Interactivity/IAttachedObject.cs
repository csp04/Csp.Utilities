using System.Windows;

namespace Csp.Utilities.Wpf.Interactivity
{
    public interface IAttachedObject
    {
        DependencyObject AssociatedObject { get; }

        void Attach(DependencyObject associatedObject);
        void Detach();
    }
}
