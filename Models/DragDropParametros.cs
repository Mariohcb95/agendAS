using AgendAS.Models.Enums;

namespace AgendAS.Models;

public class DragDropParametros
{
    public Agendamento Agendamento { get; }
    public StatusAgendamento NovoStatus { get; }

    public DragDropParametros(Agendamento agendamento, StatusAgendamento novoStatus)
    {
        Agendamento = agendamento;
        NovoStatus = novoStatus;
    }
}
