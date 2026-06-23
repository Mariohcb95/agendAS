using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgendAS.Models;
using AgendAS.Models.Enums;
using AgendAS.Repositories.Interfaces;

namespace AgendAS.Repositories.Mock;

public class AgendamentoRepositoryMock : IAgendamentoRepository
{
    public async Task<IEnumerable<Agendamento>> BuscarTodosAsync()
    {
        await Task.Delay(400);
        return DadosMock.Agendamentos.OrderBy(a => a.Data).ThenBy(a => a.Hora);
    }

    public async Task<Agendamento?> BuscarPorIdAsync(int id)
    {
        await Task.Delay(200);
        return DadosMock.Agendamentos.FirstOrDefault(a => a.Id == id);
    }

    public async Task<IEnumerable<Agendamento>> BuscarPorDataAsync(DateTime data)
    {
        await Task.Delay(300);
        return DadosMock.Agendamentos
            .Where(a => a.Data.Date == data.Date)
            .OrderBy(a => a.Hora);
    }

    public async Task<IEnumerable<Agendamento>> BuscarPorAplicadorAsync(int aplicadorId)
    {
        await Task.Delay(300);
        return DadosMock.Agendamentos
            .Where(a => a.Aplicador.Id == aplicadorId)
            .OrderBy(a => a.Data).ThenBy(a => a.Hora);
    }

    public async Task<IEnumerable<Agendamento>> BuscarPorStatusAsync(StatusAgendamento status)
    {
        await Task.Delay(300);
        return DadosMock.Agendamentos
            .Where(a => a.Status == status)
            .OrderBy(a => a.Data).ThenBy(a => a.Hora);
    }

    public async Task<Agendamento> AdicionarAsync(Agendamento agendamento)
    {
        await Task.Delay(400);
        var novoId = DadosMock.Agendamentos.Any() ? DadosMock.Agendamentos.Max(a => a.Id) + 1 : 1;
        agendamento.Id = novoId;
        agendamento.DataCriacao = DateTime.Now;
        agendamento.DataAtualizacao = DateTime.Now;
        DadosMock.Agendamentos.Add(agendamento);
        return agendamento;
    }

    public async Task<Agendamento> UpdateAsync(Agendamento agendamento) // Nota: a assinatura no repo diz AtualizarAsync, vamos ver se batem as assinaturas
    {
        // No repo é AtualizarAsync. Vamos renomear para casar perfeitamente com a interface.
        return await AtualizarAsync(agendamento);
    }

    public async Task<Agendamento> AtualizarAsync(Agendamento agendamento)
    {
        await Task.Delay(400);
        var index = DadosMock.Agendamentos.FindIndex(a => a.Id == agendamento.Id);
        if (index != -1)
        {
            agendamento.DataAtualizacao = DateTime.Now;
            DadosMock.Agendamentos[index] = agendamento;
        }
        return agendamento;
    }

    public async Task<bool> RemoverAsync(int id)
    {
        await Task.Delay(400);
        var agendamento = DadosMock.Agendamentos.FirstOrDefault(a => a.Id == id);
        if (agendamento != null)
        {
            DadosMock.Agendamentos.Remove(agendamento);
            return true;
        }
        return false;
    }
}
