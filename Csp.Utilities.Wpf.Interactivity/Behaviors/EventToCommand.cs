using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Csp.Utilities.Wpf.Interactivity.Behaviors
{
    public class EventToCommand : Behavior<FrameworkElement>
    {
        public EventToCommand()
        {
            Events = new Events();
        }

        public Events Events
        {
            get { return (Events)GetValue(EventsProperty); }
            set { SetValue(EventsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Events.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty EventsProperty =
            DependencyProperty.Register("_Events", typeof(Events), typeof(EventToCommand), new PropertyMetadata(default));


        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Command.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("_Command", typeof(ICommand), typeof(EventToCommand), new PropertyMetadata(default));


        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommandParameter.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register("_CommandParameter", typeof(object), typeof(EventToCommand), new PropertyMetadata(default));


        public Action<CustomEvent> InvokedEvent
        {
            get { return (Action<CustomEvent>)GetValue(InvokedEventProperty); }
            set { SetValue(InvokedEventProperty, value); }
        }

        // Using a DependencyProperty as the backing store for InvokedEvent.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty InvokedEventProperty =
            DependencyProperty.Register("_InvokedEvent", typeof(Action<CustomEvent>), typeof(EventToCommand), new PropertyMetadata(default));


        private IDisposable d;
        protected override void OnAttached()
        {
            if(Events != null)
            {
                Events.Register(AssociatedObject);

                d = ListenEvent.From(Events, "Invoked", (_, __) =>
                {
                    InvokedEvent?.Invoke(_ as CustomEvent);

                    if(Command.CanExecute(CommandParameter))
                    {
                        Command?.Execute(CommandParameter);
                    }
                });
            }
        }

        protected override void OnDettaching()
        {
            d.Dispose();
        }
    }
 }
