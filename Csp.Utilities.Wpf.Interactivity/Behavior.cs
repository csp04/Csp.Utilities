using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Csp.Utilities.Wpf.Interactivity
{
    public abstract class Behavior : DependencyObject, IAttachedObject
    {
        private bool _dettached = true;
        public DependencyObject AssociatedObject { get; private set; }

        public void Attach(DependencyObject associatedObject)
        {
            if (_dettached)
            {
                AssociatedObject = associatedObject;

                _dettached = false;

                OnAttached();
            }
            else
                throw new InvalidOperationException($"{nameof(AssociatedObject)} is already attached to an existing object.");
        }
        
        public void Detach()
        {
            if(!_dettached)
            {
                OnDettaching();
                AssociatedObject = null;

                _dettached = true;
            }
        }

        protected abstract void OnAttached();
        protected abstract void OnDettaching();
    
    }


    public abstract class Behavior<T> : Behavior where T : FrameworkElement
    {
        public new T AssociatedObject { get => base.AssociatedObject as T; }

        

    }
}
