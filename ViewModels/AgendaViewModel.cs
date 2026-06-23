using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AgendAS.Models;
using AgendAS.Services.Interfaces;
using AgendAS.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;

namespace AgendAS.ViewModels;

public partial class AgendaViewModel : ViewModelBase
{
    private readonly IAgendamentoService _agendamentoService;

    [ObservableProperty]
    private DateTime dataSelecionada = DateTime.Today;

    [ObservableProperty]
    private string textoPesquisa = string.Empty;

    [ObservableProperty]
    private string modoVisualizacao = "Dia"; // "Dia", "Semana", "Mês"

    public ObservableCollection<Agendamento> AgendamentosFiltrados { get; } = new();

    public AgendaViewModel(IAgendamentoService agendamentoService)
    {
        _agendamentoService = agendamentoService;
        Titulo = "Agenda";
    }

    public override async Task InicializarAsync()
    {
        await CarregarAgendamentosAsync();
    }

    // Ao mudar o texto de pesquisa na UI, recarrega com filtros aplicados
    partial void OnTextoPesquisaChanged(string value)
    {
        FiltrarAgendamentosLocais();
    }

    // Ao mudar a data selecionada na UI, recarrega
    partial void OnDataSelecionadaChanged(DateTime value)
    {
        _ = CarregarAgendamentosAsync();
    }

    // Ao mudar o modo de visualização, recarrega
    partial void OnModoVisualizacaoChanged(string value)
    {
        OnPropertyChanged(nameof(ModoDiaAtivo));
        OnPropertyChanged(nameof(ModoSemanaAtivo));
        OnPropertyChanged(nameof(ModoMesAtivo));
        _ = CarregarAgendamentosAsync();
    }

    public bool ModoDiaAtivo => ModoVisualizacao == "Dia";
    public bool ModoSemanaAtivo => ModoVisualizacao == "Semana";
    public bool ModoMesAtivo => ModoVisualizacao == "Mês";

    [RelayCommand]
    private void AlterarModo(string modo)
    {
        if (ModoVisualizacao != modo)
        {
            ModoVisualizacao = modo;
        }
    }

    [RelayCommand]
    public async Task CarregarAgendamentosAsync()
    {
        await ExecutarComLoadingAsync(async () =>
        {
            var dataFim = ModoVisualizacao switch
            {
                "Semana" => DataSelecionada.Date.AddDays(7),
                "Mês" => DataSelecionada.Date.AddMonths(1),
                _ => DataSelecionada.Date.AddDays(1) // Dia
            };

            var todos = await _agendamentoService.BuscarTodosAsync();
            var filtradosPorData = todos.Where(a => 
                a.Data.Date >= DataSelecionada.Date && 
                a.Data.Date < dataFim.Date)
                .OrderBy(a => a.Data).ThenBy(a => a.Hora)
                .ToList();

            AgendamentosFiltrados.Clear();
            foreach (var a in filtradosPorData)
            {
                AgendamentosFiltrados.Add(a);
            }

            FiltrarAgendamentosLocais();
        });
    }

    private void FiltrarAgendamentosLocais()
    {
        if (string.IsNullOrWhiteSpace(TextoPesquisa))
        {
            EstaVazio = !AgendamentosFiltrados.Any();
            return;
        }

        var pesquisa = TextoPesquisa.Trim();
        var listaAuxiliar = AgendamentosFiltrados.Where(a =>
            a.Cliente.Nome.Contains(pesquisa, StringComparison.OrdinalIgnoreCase) ||
            a.Medicamento.Contains(pesquisa, StringComparison.OrdinalIgnoreCase) ||
            a.Aplicador.Nome.Contains(pesquisa, StringComparison.OrdinalIgnoreCase) ||
            a.LocalAplicacao.Contains(pesquisa, StringComparison.OrdinalIgnoreCase))
            .ToList();

        // Filtro em memória temporário
        AgendamentosFiltrados.Clear();
        foreach (var a in listaAuxiliar)
        {
            AgendamentosFiltrados.Add(a);
        }

        EstaVazio = !AgendamentosFiltrados.Any();
    }

    [RelayCommand]
    private void NavegarDiaAnterior()
    {
        DataSelecionada = ModoVisualizacao switch
        {
            "Semana" => DataSelecionada.AddDays(-7),
            "Mês" => DataSelecionada.AddMonths(-1),
            _ => DataSelecionada.AddDays(-1)
        };
    }

    [RelayCommand]
    private void NavegarDiaSeguinte()
    {
        DataSelecionada = ModoVisualizacao switch
        {
            "Semana" => DataSelecionada.AddDays(7),
            "Mês" => DataSelecionada.AddMonths(1),
            _ => DataSelecionada.AddDays(1)
        };
    }

    [RelayCommand]
    private async Task DetalharAgendamentoAsync(Agendamento agendamento)
    {
        if (agendamento == null) return;
        await Shell.Current.GoToAsync($"NovoAgendamentoPage?AgendamentoId={agendamento.Id}");
    }

    [RelayCommand]
    private async Task NovoAgendamentoAsync()
    {
        await Shell.Current.GoToAsync("NovoAgendamentoPage");
    }
}
