using System;
using System.Reflection;

namespace Csp.Utilities
{
    /// <summary>
    /// Listens to events using reflection.
    /// </summary>
    public static class ListenEvent
    {
        /// <summary>
        /// Listens to event.
        /// </summary>
        /// <typeparam name="T">Type of the source.</typeparam>
        /// <typeparam name="TEventArg">Type of the event argument.</typeparam>
        /// <param name="source">The source where the event comes from.</param>
        /// <param name="eventName">The event's name.</param>
        /// <param name="listener">Listener method.</param>
        /// <returns>A disposable object for removing the listener from the event.</returns>
        public static IDisposable From<T, TEventArg>(T source, string eventName, Action<object, TEventArg> listener)
            where T : class
            where TEventArg : EventArgs
        {
            if (string.IsNullOrEmpty(eventName))
            {
                throw new ArgumentException(nameof(eventName));
            }

            var t = typeof(T);
            var ev = t.GetEvent(eventName);

            if (ev == null)
            {
                throw new ArgumentException("'" + eventName + "' event not found in " + t.ToString() + ".");
            }

            var handler = new DelegateHelper<TEventArg>(listener).GetHandler(ev.EventHandlerType);

            ev.AddEventHandler(source, handler);

            return Disposable.Create(() => ev.RemoveEventHandler(source, handler));
        }

        /// <summary>
        /// Listens to event.
        /// </summary>
        /// <typeparam name="T">Type of the source.</typeparam>
        /// <param name="source">The source where the event comes from.</param>
        /// <param name="eventName">The event's name.</param>
        /// <param name="listener">Listener method.</param>
        /// <returns>A disposable object for removing the listener from the event.</returns>
        public static IDisposable From<T>(T source, string eventName, Action<object, EventArgs> listener)
            where T : class
        {
            return From<T, EventArgs>(source, eventName, listener);
        }

        /// <summary>
        /// Listens to event only once. After listening, the listener will be automatically disposed.
        /// </summary>
        /// <typeparam name="T">Type of the source.</typeparam>
        /// <typeparam name="TEventArg">Type of the event argument.</typeparam>
        /// <param name="source">The source where the event comes from.</param>
        /// <param name="eventName">The event's name.</param>
        /// <param name="listener">Listener method.</param>
        public static void Once<T, TEventArg>(T source, string eventName, Action<object, TEventArg> listener)
            where T : class
            where TEventArg : EventArgs
        {
            IDisposable d = default;
            d = From<T, TEventArg>(source, eventName, (s, e) =>
            {
                listener?.Invoke(s, e);
                d.Dispose();
            });
        }

        /// <summary>
        /// Listens to event only once. After listening, the listener will be automatically disposed.
        /// </summary>
        /// <typeparam name="T">Type of the source.</typeparam>
        /// <param name="source">The source where the event comes from.</param>
        /// <param name="eventName">The event's name.</param>
        /// <param name="listener">Listener method.</param>
        public static void Once<T>(T source, string eventName, Action<object, EventArgs> listener)
            where T : class
        {
            Once<T, EventArgs>(source, eventName, listener);
        }

        /// <summary>
        /// A class helper for creating the generic method.
        /// </summary>
        /// <typeparam name="TEventArg">Type of the event argument.</typeparam>
        private sealed class DelegateHelper<TEventArg>
        {
            private Action<object, TEventArg> _handle;

            public DelegateHelper(Action<object, TEventArg> handle)
            {
                _handle = handle;
            }

            /// <summary>
            /// A temporary method that will be used for creating the generic method.
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="arg"></param>
            private void Handle(object sender, TEventArg arg)
            {
                _handle.Invoke(sender, arg);
            }

            /// <summary>
            /// Creates the handler that will match the event handler type.
            /// </summary>
            /// <param name="eventType">Type of the event handler.</param>
            /// <returns>The handler.</returns>
            public Delegate GetHandler(Type eventType) =>
                Delegate.CreateDelegate(eventType, this, this.GetType().GetMethod("Handle", BindingFlags.NonPublic | BindingFlags.Instance));

        }


    }
}
