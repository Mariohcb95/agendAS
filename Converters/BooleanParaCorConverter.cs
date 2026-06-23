using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace AgendAS.Converters;

public class BooleanParaCorConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        bool temErro = value is bool val && val;

        if (temErro)
        {
            if (Application.Current?.Resources.TryGetValue("VermelhoCancelado", out var corErro) == true)
            {
                return corErro;
            }
            return Colors.Red;
        }

        // Se não tiver erro, retorna a cor de borda apropriada ao tema ativo (Light/Dark)
        bool ehTemaEscuro = Application.Current?.RequestedTheme == AppTheme.Dark;
        string chaveBorda = ehTemaEscuro ? "BordaEscura" : "BordaClara";

        if (Application.Current?.Resources.TryGetValue(chaveBorda, out var corPadrao) == true)
        {
            return corPadrao;
        }

        return Colors.LightGray;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
