using System;
using AgendAS.Models;

namespace AgendAS.Validators;

public static class AgendamentoValidator
{
    public static (bool Valido, string Erro) Validar(Agendamento agendamento)
    {
        if (agendamento.Cliente == null || agendamento.Cliente.Id <= 0)
        {
            return (false, "Por favor, selecione um cliente.");
        }

        if (agendamento.Aplicador == null || agendamento.Aplicador.Id <= 0)
        {
            return (false, "Por favor, selecione um aplicador.");
        }

        if (string.IsNullOrWhiteSpace(agendamento.Medicamento))
        {
            return (false, "O nome do medicamento é obrigatório.");
        }

        if (string.IsNullOrWhiteSpace(agendamento.LocalAplicacao))
        {
            return (false, "O local de aplicação é obrigatório.");
        }

        if (agendamento.Data.Date < DateTime.Today)
        {
            return (false, "A data do agendamento não pode ser no passado.");
        }

        if (agendamento.Hora == TimeSpan.Zero)
        {
            // Nota: TimeSpan.Zero pode ser válido em alguns cenários (meia-noite), mas assumimos que o usuário precisa selecionar.
            // Para não travar caso queira agendar à meia-noite, validamos apenas se o horário está fora do padrão se necessário.
        }

        return (true, string.Empty);
    }
}
