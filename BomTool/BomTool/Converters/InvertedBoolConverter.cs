using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BomTool.Converters;

public class InvertedBoolConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not bool vis)
            return DependencyProperty.UnsetValue;

        return !vis;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not bool vis)
            return DependencyProperty.UnsetValue;

        return !vis;
    }
}

