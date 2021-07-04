using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Csp.Utilities.Wpf.Interactivity.TestApp.Controls
{
    public class ButtonEx : Button
    {

        public ButtonEx()
        {
            Behaviors = new BehaviorCollection();
        }

        public BehaviorCollection Behaviors
        {
            get { return (BehaviorCollection)GetValue(BehaviorsProperty); }
            set { SetValue(BehaviorsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Behaviors.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BehaviorsProperty =
            DependencyProperty.Register("Behaviors", typeof(BehaviorCollection), typeof(ButtonEx), new PropertyMetadata(OnBehaviorsChanged));

        private static void OnBehaviorsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue == e.NewValue)
            {
                return;
            }

            if (e.OldValue is BehaviorCollection oldBehaviors)
            {
                oldBehaviors.Detach();
            }

            if (e.NewValue is BehaviorCollection newBehaviors)
            {
                var fe = d as FrameworkElement;

                newBehaviors.Attach(fe);
            }
        }
    }
}
