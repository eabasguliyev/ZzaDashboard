﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ZzaDashboard.Converters
{
    public class NegatableBooleanToVisibilityConverter:IValueConverter
    {
        public NegatableBooleanToVisibilityConverter()
        {
            FalseVisibility = Visibility.Collapsed;
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool bVal;

            bool result = bool.TryParse(value.ToString(), out bVal);

            if (!result) return value;

            if (bVal && !Negate)
                return Visibility.Visible;

            if (bVal && Negate)
                return FalseVisibility;

            if (!bVal && Negate)
                return Visibility.Visible;

            if (!bVal && !Negate)
                return FalseVisibility;

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public bool Negate { get; set; }
        public Visibility FalseVisibility { get; set; }
    }
}