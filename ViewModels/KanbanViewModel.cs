using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AgendAS.Components;
using AgendAS.Models;
using AgendAS.Models.Enums;
using AgendAS.Services.Interfaces;
using AgendAS.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AgendAS.ViewModels;

public partial class KanbanViewModel : ViewModelBase
{
    private readonly IAgendamentoService _agendamentoService;
    private readonly INotificacaoService _notificacaoService;

    // Coleções para cada coluna do Kanban
    public ObservableCollection<Agendamento> AgendadosObservable { get; } = new();
    public ObservableCollection<Agendamento> ConfirmadosObservable { get; } = new();
    public ObservableCollection<Agendamento> EmAndamentoObservable { get; } = new();
    public ObservableCollection<Agendamento> ConcluidosObservable { get; } = new();
    public ObservableCollection<Agendamento> CanceladosObservable { get; } = new();

    // Contadores
    [ObservableProperty] private int totalAgendados;
    [ObservableProperty] private int totalConfirmados;
    [ObservableProperty] private int totalEmAndamento;
    [ObservableProperty] private int totalConcluidos;
    [ObservableProperty] private int totalCancelados;

    public KanbanViewModel(IAgendamentoService agendamentoService, INotificacaoService notificacaoService)
    {
        _agendamentoService = agendamentoService;
        _notificacaoService = notificacaoService;
        Titulo = "Kanban";
    }

    public override async Task InicializarAsync()
    {
        await CarregarKanbanAsync();
    }

    [RelayCommand]
    public async Task CarregarKanbanAsync()
    {
        await ExecutarComLoadingAsync(async () =>
        {
            var agendamentos = await _agendamentoService.BuscarTodosAsync();

            // Limpa as listas
            AgendadosObservable.Clear();
            ConfirmadosObservable.Clear();
            EmAndamentoObservable.Clear();
            ConcluidosObservable.Clear();
            CanceladosObservable.Clear();

            // Distribui nas colunas
            foreach (var a in agendamentos)
            {
                switch (a.Status)
                {
                    case StatusAgendamento.Agendado:
                        AgendadosObservable.Add(a);
                        break;
                    case StatusAgendamento.Confirmado:
                        ConfirmadosObservable.Add(a);
                        break;
                    case StatusAgendamento.EmAndamento:
                        EmAndamentoObservable.Add(a);
                        break;
                    case StatusAgendamento.Concluido:
                        ConcluidosObservable.Add(a);
                        break;
                    case StatusAgendamento.Cancelado:
                        CanceladosObservable.Add(a);
                        break;
                }
            }

            AtualizarContadores();
        });
    }

    [RelayCommand]
    private async Task MoverAgendamentoAsync(DragDropParametros parametros)
    {
        if (parametros == null || parametros.Agendamento == null) return;

        var agendamento = parametros.Agendamento;
        var novoStatus = parametros.NovoStatus;

        if (agendamento.Status == novoStatus) return; // Mesmo status, nenhuma ação necessária

        var statusAntigo = agendamento.Status;

        try
        {
            // 1. Atualizar status via service (que valida regras e atualiza logs)
            await _agendamentoService.AtualizarStatusAsync(agendamento.Id, novoStatus);
            
            // 2. Mudar localmente
            RemoverDaListaLocal(agendamento, statusAntigo);
            
            agendamento.Status = novoStatus;
            AdicionarNaListaLocal(agendamento, novoStatus);

            AtualizarContadores();

            // Enviar feedback visual ao usuário
            string statusTexto = novoStatus switch
            {
                StatusAgendamento.Agendado => "Agendado",
                StatusAgendamento.Confirmado => "Confirmado",
                StatusAgendamento.EmAndamento => "Em Andamento",
                StatusAgendamento.Concluido => "Concluído",
                StatusAgendamento.Cancelado => "Cancelado",
                _ => novoStatus.ToString()
            };
            await _notificacaoService.EnviarToastAsync($"Status alterado para: {statusTexto}");
        }
        catch (Exception ex)
        {
            await _notificacaoService.EnviarToastAsync($"Erro ao mover agendamento: {ex.Message}");
            // Recarrega o Kanban para restaurar o estado correto das listas caso ocorra erro
            await CarregarKanbanAsync();
        }
    }

    private void RemoverDaListaLocal(Agendamento agendamento, StatusAgendamento status)
    {
        ObservableCollection<Agendamento> listaAlvo = ObterListaPorStatus(status);
        var item = listaAlvo.FirstOrDefault(a => a.Id == agendamento.Id);
        if (item != null)
        {
            listaAlvo.Remove(item);
        }
    }

    private void AdicionarNaListaLocal(Agendamento agendamento, StatusAgendamento status)
    {
        ObservableCollection<Agendamento> listaAlvo = ObterListaPorStatus(status);
        listaAlvo.Add(agendamento);
    }

    private ObservableCollection<Agendamento> ObterListaPorStatus(StatusAgendamento status)
    {
        return status switch
        {
            StatusAgendamento.Agendado => AgendadosObservable,
            StatusAgendamento.Confirmado => ConfirmadosObservable,
            StatusAgendamento.EmAndamento => EmAndamentoObservable,
            StatusAgendamento.Concluido => ConcluidosObservable,
            StatusAgendamento.Cancelado => CanceladosObservable,
            _ => throw new ArgumentException("Status inválido")
        };
    }

    private void AtualizarContadores()
    {
        TotalAgendados = AgendadosObservable.Count;
        TotalConfirmados = ConfirmadosObservable.Count;
        TotalEmAndamento = EmAndamentoObservable.Count;
        TotalConcluidos = ConcluidosObservable.Count;
        TotalCancelados = CanceladosObservable.Count;
    }
}
