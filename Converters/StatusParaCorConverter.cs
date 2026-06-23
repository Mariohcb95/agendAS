using System;
using System.Globalization;
using Microsoft.Maui.Controls;
using AgendAS.Models.Enums;

namespace AgendAS.Converters;

public class StatusParaCorConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is StatusAgendamento status)
        {
            string chaveCor = status switch
            {
                StatusAgendamento.Agendado => "AzulAgendado",
                StatusAgendamento.Confirmado => "VerdeConfirmado",
                StatusAgendamento.EmAndamento => "AmareloEmAndamento",
                StatusAgendamento.Concluido => "RoxoConcluido",
                StatusAgendamento.Cancelado => "VermelhoCancelado",
                _ => "TextoSecundarioCor"
            };

            if (Application.Current?.Resources.TryGetValue(chaveCor, out var cor) == true)
            {
                return cor;
            }
        }

        return Colors.SlateGray;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
