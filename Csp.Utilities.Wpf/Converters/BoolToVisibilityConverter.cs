using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Csp.Utilities.Wpf.Converters
{

    public class BoolToVisibilityConverter : BoolToAnyValueConverter<Visibility>
    {
        public override Visibility TrueValue { get; set; } = Visibility.Visible;
        public override Visibility FalseValue { get; set; } = Visibility.Collapsed;
    }
}
