using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;

namespace Csp.Utilities.Wpf.Interactivity.Behaviors
{
    public sealed class Events : ObservableCollection<CustomEvent>, IEventInvoke, IDisposable
    {
        private FrameworkElement eventSource = default;

        private readonly IDisposable d;

        public event EventHandler Invoked;
        private void OnInvoke(object sender, EventArgs eventArgs) => Invoked?.Invoke(sender, eventArgs);

        public Events()
        {
            d = ListenEvent.From<Events, NotifyCollectionChangedEventArgs>(this, "CollectionChanged", (_, e) =>
            {
                if (e.OldItems != null)
                {
                    foreach (CustomEvent ev in e.OldItems)
                    {
                        ev.Invoked -= Event_Invoked;
                        ev.Dispose();
                    }
                }

                if (e.NewItems != null && eventSource != null)
                {
                    foreach (CustomEvent ev in e.OldItems)
                    {
                        ev.Register(eventSource);
                        ev.Invoked += Event_Invoked;
                    }
                }
            });
        }

        private void Event_Invoked(object sender, EventArgs e)
        {
            OnInvoke(sender, e);
        }

        internal void Register(FrameworkElement eventSource)
        {
            this.eventSource = eventSource;
            foreach(var ev in this)
            {
                ev.Register(eventSource);
                ev.Invoked += Event_Invoked;
            }
        }

        public void Dispose()
        {
            foreach (var ev in this)
            {
                ev.Invoked -= Event_Invoked;
                ev.Dispose();
            }
            d.Dispose();
        }
    }
}
