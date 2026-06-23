using System;
using System.Globalization;
using Microsoft.Maui.Controls;
using AgendAS.Models.Enums;

namespace AgendAS.Converters;

public class StatusParaTextoConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is StatusAgendamento status)
        {
            return status switch
            {
                StatusAgendamento.Agendado => "Agendado",
                StatusAgendamento.Confirmado => "Confirmado",
                StatusAgendamento.EmAndamento => "Em Andamento",
                StatusAgendamento.Concluido => "Concluído",
                StatusAgendamento.Cancelado => "Cancelado",
                _ => status.ToString()
            };
        }

        return string.Empty;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
