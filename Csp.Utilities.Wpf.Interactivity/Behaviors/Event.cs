using System.Windows;

namespace Csp.Utilities.Wpf.Interactivity.Behaviors
{
    public sealed class Event : CustomEvent
    {
        public string Name { get; set; }

        protected override void OnRegister(FrameworkElement eventSource)
        {
            Cleanup.Add(ListenEvent.From(eventSource, Name, (_, e) => OnInvoke(e)));
        }
    }
}
