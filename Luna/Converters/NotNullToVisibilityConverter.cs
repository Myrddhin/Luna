using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Loki.Common;

namespace Luna.UI.Converters
{
    public class NotNullToVisibilityConverter : IValueConverter
    {
        public Visibility TrueValue { get; set; }

        public Visibility FalseValue { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.IsNullOrEmpty(value.SafeToString()) ? FalseValue : TrueValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}