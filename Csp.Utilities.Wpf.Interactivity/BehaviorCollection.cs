using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;

namespace Csp.Utilities.Wpf.Interactivity
{
    public sealed class BehaviorCollection : ObservableCollection<Behavior>, IAttachedObject
    {
        private IDisposable d;
        private bool _dettached = true;
        public DependencyObject AssociatedObject { get; private set; }

        public void Attach(DependencyObject associatedObject)
        {
            if (!_dettached) { throw new InvalidOperationException($"{nameof(AssociatedObject)} is already attached to an existing object."); }

            AssociatedObject = associatedObject;

            _dettached = false;

            foreach (var behavior in this)
            {
                behavior.Attach(associatedObject);
            }

            d = ListenEvent.From<BehaviorCollection, NotifyCollectionChangedEventArgs>(this, "CollectionChanged",
                            (_, e) =>
                            {

                                if (e.OldItems != null)
                                {
                                    foreach (Behavior behavior in e.OldItems)
                                    {
                                        behavior.Detach();
                                    }
                                }

                                if (e.NewItems != null && AssociatedObject != null)
                                {
                                    foreach (Behavior behavior in e.NewItems)
                                    {
                                        behavior.Attach(AssociatedObject);
                                    }
                                }
                            });
        }

        

        public void Detach()
        {
            if(!_dettached)
            {
                foreach (var behavior in this)
                {
                    behavior.Detach();
                }
                d.Dispose();

                AssociatedObject = null;

                _dettached = true;
            }
            
        }
    }
}
