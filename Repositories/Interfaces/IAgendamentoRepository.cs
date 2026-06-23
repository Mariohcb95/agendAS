using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgendAS.Models;
using AgendAS.Models.Enums;

namespace AgendAS.Repositories.Interfaces;

public interface IAgendamentoRepository
{
    Task<IEnumerable<Agendamento>> BuscarTodosAsync();
    Task<Agendamento?> BuscarPorIdAsync(int id);
    Task<IEnumerable<Agendamento>> BuscarPorDataAsync(DateTime data);
    Task<IEnumerable<Agendamento>> BuscarPorAplicadorAsync(int aplicadorId);
    Task<IEnumerable<Agendamento>> BuscarPorStatusAsync(StatusAgendamento status);
    Task<Agendamento> AdicionarAsync(Agendamento agendamento);
    Task<Agendamento> AtualizarAsync(Agendamento agendamento);
    Task<bool> RemoverAsync(int id);
}
