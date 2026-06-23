using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace AgendAS.Converters;

public class DataParaTextoConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is DateTime data)
        {
            var hoje = DateTime.Today;
            var dataVerificacao = data.Date;

            if (dataVerificacao == hoje)
            {
                return "Hoje";
            }
            if (dataVerificacao == hoje.AddDays(1))
            {
                return "Amanhã";
            }
            if (dataVerificacao == hoje.AddDays(-1))
            {
                return "Ontem";
            }

            // Exibir dia da semana e data curta
            string diaSemana = culture.TextInfo.ToTitleCase(data.ToString("dddd", culture));
            return $"{diaSemana}, {data.ToString("dd/MM/yyyy", culture)}";
        }

        return string.Empty;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
