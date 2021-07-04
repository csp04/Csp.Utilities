using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Csp.Utilities.Wpf.Interactivity
{
    public static class Interaction
    {
        internal static bool IsInDesignMode => DesignerProperties.GetIsInDesignMode(new DependencyObject());


        #region Behaviors Dependency Property               
        public static BehaviorCollection GetBehaviors(DependencyObject obj)
        {
            var behaviors = (BehaviorCollection)obj.GetValue(BehaviorsProperty);
            if(behaviors == null)
            {
                behaviors = new BehaviorCollection();
                obj.SetValue(BehaviorsProperty, behaviors);

                var fe = obj as FrameworkElement;

                if(fe != null)
                {
                    fe.Loaded -= FrameworkElement_Loaded;
                    fe.Loaded += FrameworkElement_Loaded;
                }
            }
            return behaviors;
        }


        private static void FrameworkElement_Unloaded(object sender, RoutedEventArgs e)
        {
            if(!IsInDesignMode)
            {
                var d = sender as FrameworkElement;
                d.Unloaded -= FrameworkElement_Unloaded;
                GetBehaviors(d).Detach();
            }
            
        }

        private static void FrameworkElement_Loaded(object sender, RoutedEventArgs e)
        {
            if(!IsInDesignMode)
            {
                var d = sender as FrameworkElement;
                d.Unloaded += FrameworkElement_Unloaded;
                GetBehaviors(d).Attach(d);
            }
        }


        // Using a DependencyProperty as the backing store for Behaviors.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty BehaviorsProperty =
            DependencyProperty.RegisterAttached("_Behaviors", typeof(BehaviorCollection), typeof(Interaction), new FrameworkPropertyMetadata(default));

        //private static void OnBehaviorsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    if(e.OldValue == e.NewValue)
        //    {
        //        return;
        //    }

        //    if (e.OldValue is BehaviorCollection oldBehaviors)
        //    {
        //        oldBehaviors.Detach();
        //    }

        //    if (e.NewValue is BehaviorCollection newBehaviors && !IsInDesignMode)
        //    {
        //        var fe = d as FrameworkElement;

        //        newBehaviors.Attach(fe);
        //    }
        //}
        #endregion

    }
}
