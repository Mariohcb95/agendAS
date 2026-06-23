using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AgendAS.Models;
using AgendAS.Models.Enums;
using AgendAS.Services.Interfaces;
using AgendAS.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AgendAS.ViewModels;

public partial class DashboardViewModel : ViewModelBase
{
    private readonly IAgendamentoService _agendamentoService;
    private readonly IAplicadorService _aplicadorService;
    private readonly IUsuarioService _usuarioService;

    [ObservableProperty]
    private string saudacaoUsuario = "Olá!";

    [ObservableProperty]
    private string totalAplicacoesHoje = "0";

    [ObservableProperty]
    private string totalAplicacoesConcluidas = "0";

    [ObservableProperty]
    private string totalAplicacoesPendentes = "0";

    [ObservableProperty]
    private string totalAplicadoresDisponiveis = "0";

    public ObservableCollection<Agendamento> ProximasAplicacoes { get; } = new();

    public DashboardViewModel(
        IAgendamentoService agendamentoService,
        IAplicadorService aplicadorService,
        IUsuarioService usuarioService)
    {
        _agendamentoService = agendamentoService;
        _aplicadorService = aplicadorService;
        _usuarioService = usuarioService;
        Titulo = "Dashboard";
    }

    public override async Task InicializarAsync()
    {
        await CarregarDadosAsync();
    }

    [RelayCommand]
    private async Task CarregarDadosAsync()
    {
        await ExecutarComLoadingAsync(async () =>
        {
            // 1. Obter saudação personalizada
            var usuario = await _usuarioService.ObterUsuarioLogadoAsync();
            if (usuario != null)
            {
                SaudacaoUsuario = $"Olá, {usuario.NomeCompleto.Split(' ')[0]}";
            }

            // 2. Buscar agendamentos e aplicadores
            var todosAgendamentos = await _agendamentoService.BuscarTodosAsync();
            var todosAplicadores = await _aplicadorService.BuscarTodosAsync();

            var hoje = DateTime.Today;

            // 3. Filtrar dados do dia
            var agendamentosHoje = todosAgendamentos.Where(a => a.Data.Date == hoje).ToList();
            
            TotalAplicacoesHoje = agendamentosHoje.Count.ToString();
            TotalAplicacoesConcluidas = agendamentosHoje.Count(a => a.Status == StatusAgendamento.Concluido).ToString();
            TotalAplicacoesPendentes = agendamentosHoje.Count(a => 
                a.Status == StatusAgendamento.Agendado || 
                a.Status == StatusAgendamento.Confirmado || 
                a.Status == StatusAgendamento.EmAndamento).ToString();

            // 4. Calcular aplicadores disponíveis
            TotalAplicadoresDisponiveis = todosAplicadores.Count(a => a.StatusDisponibilidade == StatusDisponibilidade.Disponivel).ToString();

            // 5. Próximas 5 aplicações (hoje em diante, ordenado por data/hora)
            var proximas = todosAgendamentos
                .Where(a => a.DataHoraCompleta >= DateTime.Now && a.Status != StatusAgendamento.Cancelado && a.Status != StatusAgendamento.Concluido)
                .Take(5)
                .ToList();

            ProximasAplicacoes.Clear();
            foreach (var ag in proximas)
            {
                ProximasAplicacoes.Add(ag);
            }

            EstaVazio = !ProximasAplicacoes.Any();
        });
    }

    [RelayCommand]
    private async Task DetalharAgendamentoAsync(Agendamento agendamento)
    {
        if (agendamento == null) return;
        // Navega para o NovoAgendamentoPage passando o id para edição
        await Shell.Current.GoToAsync($"NovoAgendamentoPage?AgendamentoId={agendamento.Id}");
    }
}
