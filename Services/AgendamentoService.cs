using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgendAS.Models;
using AgendAS.Models.Enums;
using AgendAS.Repositories.Interfaces;
using AgendAS.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace AgendAS.Services;

public class AgendamentoService : IAgendamentoService
{
    private readonly IAgendamentoRepository _agendamentoRepository;
    private readonly INotificacaoService _notificacaoService;
    private readonly ILogger<AgendamentoService> _logger;

    public AgendamentoService(
        IAgendamentoRepository agendamentoRepository,
        INotificacaoService notificacaoService,
        ILogger<AgendamentoService> logger)
    {
        _agendamentoRepository = agendamentoRepository;
        _notificacaoService = notificacaoService;
        _logger = logger;
    }

    public async Task<IEnumerable<Agendamento>> BuscarTodosAsync()
    {
        _logger.LogInformation("Buscando todos os agendamentos");
        return await _agendamentoRepository.BuscarTodosAsync();
    }

    public async Task<Agendamento?> BuscarPorIdAsync(int id)
    {
        _logger.LogInformation("Buscando agendamento por Id: {Id}", id);
        return await _agendamentoRepository.BuscarPorIdAsync(id);
    }

    public async Task<IEnumerable<Agendamento>> BuscarPorDataAsync(DateTime data)
    {
        _logger.LogInformation("Buscando agendamentos para a data: {Data:dd/MM/yyyy}", data);
        return await _agendamentoRepository.BuscarPorDataAsync(data);
    }

    public async Task<IEnumerable<Agendamento>> BuscarPorAplicadorAsync(int aplicadorId)
    {
        _logger.LogInformation("Buscando agendamentos do aplicador de Id: {AplicadorId}", aplicadorId);
        return await _agendamentoRepository.BuscarPorAplicadorAsync(aplicadorId);
    }

    public async Task<IEnumerable<Agendamento>> BuscarPorStatusAsync(StatusAgendamento status)
    {
        _logger.LogInformation("Buscando agendamentos com status: {Status}", status);
        return await _agendamentoRepository.BuscarPorStatusAsync(status);
    }

    public async Task<bool> ValidarConflitoHorarioAsync(int aplicadorId, DateTime data, TimeSpan hora, int? agendamentoIdExcluir = null)
    {
        _logger.LogInformation("Validando conflito de horário para aplicador {AplicadorId} em {Data:dd/MM/yyyy} {Hora}", 
            aplicadorId, data, hora);
        
        var agendamentos = await _agendamentoRepository.BuscarPorAplicadorAsync(aplicadorId);
        
        // Regra de Negócio: Não permitir mais de um agendamento para o mesmo aplicador no mesmo horário.
        // Ignoramos agendamentos cancelados e o próprio agendamento atual se for edição (para não colidir consigo mesmo)
        var possuiConflito = agendamentos.Any(a =>
            a.Data.Date == data.Date &&
            a.Hora == hora &&
            a.Status != StatusAgendamento.Cancelado &&
            a.Id != (agendamentoIdExcluir ?? -1));

        if (possuiConflito)
        {
            _logger.LogWarning("Conflito detectado: o aplicador {AplicadorId} já possui agendamento em {Data:dd/MM/yyyy} {Hora}", 
                aplicadorId, data, hora);
        }

        return possuiConflito;
    }

    public async Task<Agendamento> SalvarAsync(Agendamento agendamento)
    {
        // 1. Validar regra obrigatória antes de qualquer salvamento
        bool conflito = await ValidarConflitoHorarioAsync(
            agendamento.Aplicador.Id, 
            agendamento.Data, 
            agendamento.Hora, 
            agendamento.Id > 0 ? agendamento.Id : null);

        if (conflito)
        {
            throw new InvalidOperationException("Conflito de agenda: O aplicador selecionado já possui um compromisso agendado para este dia e horário.");
        }

        Agendamento resultado;
        if (agendamento.Id <= 0)
        {
            _logger.LogInformation("Salvando novo agendamento");
            resultado = await _agendamentoRepository.AdicionarAsync(agendamento);
            
            // Agendar notificação de lembrete para a nova aplicação se for no futuro
            if (resultado.DataHoraCompleta > DateTime.Now && resultado.Status != StatusAgendamento.Cancelado)
            {
                await _notificacaoService.AgendarNotificacaoAplicacaoAsync(resultado);
            }
        }
        else
        {
            _logger.LogInformation("Atualizando agendamento de Id: {Id}", agendamento.Id);
            resultado = await _agendamentoRepository.AtualizarAsync(agendamento);

            // Ajustar notificações conforme o novo horário ou status
            if (resultado.Status == StatusAgendamento.Cancelado || resultado.Status == StatusAgendamento.Concluido)
            {
                await _notificacaoService.CancelarNotificacaoAplicacaoAsync(resultado.Id);
            }
            else if (resultado.DataHoraCompleta > DateTime.Now)
            {
                await _notificacaoService.CancelarNotificacaoAplicacaoAsync(resultado.Id);
                await _notificacaoService.AgendarNotificacaoAplicacaoAsync(resultado);
            }
        }

        return resultado;
    }

    public async Task<bool> ExcluirAsync(int id)
    {
        _logger.LogInformation("Excluindo agendamento de Id: {Id}", id);
        var removido = await _agendamentoRepository.RemoverAsync(id);
        if (removido)
        {
            await _notificacaoService.CancelarNotificacaoAplicacaoAsync(id);
        }
        return removido;
    }

    public async Task<Agendamento> AtualizarStatusAsync(int agendamentoId, StatusAgendamento status)
    {
        _logger.LogInformation("Alterando status do agendamento {Id} para {Status}", agendamentoId, status);
        
        var agendamento = await _agendamentoRepository.BuscarPorIdAsync(agendamentoId);
        if (agendamento == null)
        {
            throw new KeyNotFoundException($"Agendamento com Id {agendamentoId} não encontrado.");
        }

        agendamento.Status = status;
        var resultado = await _agendamentoRepository.AtualizarAsync(agendamento);

        // Cancelar ou reagendar notificações se necessário
        if (status == StatusAgendamento.Cancelado || status == StatusAgendamento.Concluido)
        {
            await _notificacaoService.CancelarNotificacaoAplicacaoAsync(agendamentoId);
        }

        return resultado;
    }
}
