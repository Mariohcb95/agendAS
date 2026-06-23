using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace AgendAS.Converters;

public class StringPreenchidaParaBooleanConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string texto)
        {
            return !string.IsNullOrWhiteSpace(texto);
        }

        return false;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
