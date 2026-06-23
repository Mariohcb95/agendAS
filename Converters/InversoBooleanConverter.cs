using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace AgendAS.Converters;

public class InversoBooleanConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool booleanValue)
        {
            return !booleanValue;
        }

        return false;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool booleanValue)
        {
            return !booleanValue;
        }

        return false;
    }
}
