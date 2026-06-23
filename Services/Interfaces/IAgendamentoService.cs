using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgendAS.Models;
using AgendAS.Models.Enums;

namespace AgendAS.Services.Interfaces;

public interface IAgendamentoService
{
    Task<IEnumerable<Agendamento>> BuscarTodosAsync();
    Task<Agendamento?> BuscarPorIdAsync(int id);
    Task<IEnumerable<Agendamento>> BuscarPorDataAsync(DateTime data);
    Task<IEnumerable<Agendamento>> BuscarPorAplicadorAsync(int aplicadorId);
    Task<IEnumerable<Agendamento>> BuscarPorStatusAsync(StatusAgendamento status);
    Task<Agendamento> SalvarAsync(Agendamento agendamento);
    Task<bool> ExcluirAsync(int id);
    Task<bool> ValidarConflitoHorarioAsync(int aplicadorId, DateTime data, TimeSpan hora, int? agendamentoIdExcluir = null);
    Task<Agendamento> AtualizarStatusAsync(int agendamentoId, StatusAgendamento status);
}
