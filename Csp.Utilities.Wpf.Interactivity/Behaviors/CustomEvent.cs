using System;
using System.Windows;

namespace Csp.Utilities.Wpf.Interactivity.Behaviors
{
    public abstract class CustomEvent : IEventInvoke, IDisposable
    {
        protected CompositeDisposable Cleanup = new CompositeDisposable();

        private bool disposedValue;

        public event EventHandler Invoked;
        protected virtual void OnInvoke(EventArgs eventArgs) => Invoked?.Invoke(this, eventArgs);
        protected virtual void OnInvoke() => OnInvoke(EventArgs.Empty);

        internal void Register(FrameworkElement eventSource) => OnRegister(eventSource);

        protected abstract void OnRegister(FrameworkElement eventSource);

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Cleanup.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~CustomEvent()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
