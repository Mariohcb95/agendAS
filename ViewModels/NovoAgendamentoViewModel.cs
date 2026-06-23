using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AgendAS.Models;
using AgendAS.Models.Enums;
using AgendAS.Services.Interfaces;
using AgendAS.Validators;
using AgendAS.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;

namespace AgendAS.ViewModels;

[QueryProperty(nameof(AgendamentoIdStr), "AgendamentoId")]
public partial class NovoAgendamentoViewModel : ViewModelBase, IQueryAttributable
{
    private readonly IAgendamentoService _agendamentoService;
    private readonly IClienteService _clienteService;
    private readonly IAplicadorService _aplicadorService;
    private readonly INotificacaoService _notificacaoService;

    private int _agendamentoId = 0;
    private bool _inicializandoEdicao = false;

    [ObservableProperty] private string? agendamentoIdStr;

    // Campos do formulário
    [ObservableProperty] private Cliente? clienteSelecionado;
    [ObservableProperty] private Aplicador? aplicadorSelecionado;
    [ObservableProperty] private string medicamento = string.Empty;
    [ObservableProperty] private string localAplicacao = "Sala de Injeção Premium";
    [ObservableProperty] private DateTime data = DateTime.Today;
    [ObservableProperty] private TimeSpan hora = new TimeSpan(9, 0, 0);
    [ObservableProperty] private string detalhesAplicacao = string.Empty;
    [ObservableProperty] private string observacoes = string.Empty;
    [ObservableProperty] private StatusAgendamento status = StatusAgendamento.Agendado;

    // Erros e Conflitos
    [ObservableProperty] private string? erroCliente;
    [ObservableProperty] private string? erroAplicador;
    [ObservableProperty] private string? erroMedicamento;
    [ObservableProperty] private string? erroLocal;
    [ObservableProperty] private string? erroData;
    
    [ObservableProperty] private bool possuiConflito;
    [ObservableProperty] private string mensagemConflito = string.Empty;

    // Listas para Pickers
    public ObservableCollection<Cliente> Clientes { get; } = new();
    public ObservableCollection<Aplicador> Aplicadores { get; } = new();
    public ObservableCollection<StatusAgendamento> ListaStatus { get; } = new();

    public bool ModoEdicao => _agendamentoId > 0;

    public NovoAgendamentoViewModel(
        IAgendamentoService agendamentoService,
        IClienteService clienteService,
        IAplicadorService aplicadorService,
        INotificacaoService notificacaoService)
    {
        _agendamentoService = agendamentoService;
        _clienteService = clienteService;
        _aplicadorService = aplicadorService;
        _notificacaoService = notificacaoService;

        Titulo = "Novo Agendamento";

        // Preenche os status possíveis
        foreach (StatusAgendamento st in Enum.GetValues(typeof(StatusAgendamento)))
        {
            ListaStatus.Add(st);
        }
    }

    public override async Task InicializarAsync()
    {
        await ExecutarComLoadingAsync(async () =>
        {
            // Carregar dados dos Pickers
            var clientes = await _clienteService.BuscarTodosAsync();
            Clientes.Clear();
            foreach (var c in clientes) Clientes.Add(c);

            var aplicadores = await _aplicadorService.BuscarTodosAsync();
            Aplicadores.Clear();
            foreach (var a in aplicadores) Aplicadores.Add(a);
        });

        if (ModoEdicao)
        {
            await CarregarEdicaoAsync();
        }
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("AgendamentoId", out var idValue))
        {
            if (int.TryParse(idValue?.ToString(), out var id))
            {
                _agendamentoId = id;
            }
        }
    }

    private async Task CarregarEdicaoAsync()
    {
        _inicializandoEdicao = true;
        Titulo = "Editar Agendamento";

        await ExecutarComLoadingAsync(async () =>
        {
            var ag = await _agendamentoService.BuscarPorIdAsync(_agendamentoId);
            if (ag != null)
            {
                Medicamento = ag.Medicamento;
                LocalAplicacao = ag.LocalAplicacao;
                Data = ag.Data;
                Hora = ag.Hora;
                DetalhesAplicacao = ag.DetalhesAplicacao;
                Observacoes = ag.Observacoes;
                Status = ag.Status;

                // Selecionar os objetos correspondentes nas listas do Picker
                ClienteSelecionado = Clientes.FirstOrDefault(c => c.Id == ag.Cliente.Id);
                AplicadorSelecionado = Aplicadores.FirstOrDefault(a => a.Id == ag.Aplicador.Id);
            }
        });

        _inicializandoEdicao = false;
        await VerificarConflitoHorarioAsync();
    }

    // Monitorar mudanças para validação em tempo real de conflitos de horário
    partial void OnAplicadorSelecionadoChanged(Aplicador? value) => _ = VerificarConflitoHorarioAsync();
    partial void OnDataChanged(DateTime value) => _ = VerificarConflitoHorarioAsync();
    partial void OnHoraChanged(TimeSpan value) => _ = VerificarConflitoHorarioAsync();

    private async Task VerificarConflitoHorarioAsync()
    {
        if (_inicializandoEdicao || AplicadorSelecionado == null)
        {
            PossuiConflito = false;
            MensagemConflito = string.Empty;
            return;
        }

        // Valida se já existe agendamento ativo para este aplicador neste horário
        var conflito = await _agendamentoService.ValidarConflitoHorarioAsync(
            AplicadorSelecionado.Id,
            Data,
            Hora,
            _agendamentoId > 0 ? _agendamentoId : null);

        PossuiConflito = conflito;
        if (conflito)
        {
            MensagemConflito = $"Atenção: O aplicador {AplicadorSelecionado.Nome} já possui aplicação agendada para {Data:dd/MM/yyyy} às {Hora:hh\\:mm}.";
        }
        else
        {
            MensagemConflito = string.Empty;
        }
    }

    [RelayCommand]
    private async Task SalvarAsync()
    {
        LimparErros();

        var agendamento = new Agendamento
        {
            Id = _agendamentoId,
            Cliente = ClienteSelecionado ?? new(),
            Aplicador = AplicadorSelecionado ?? new(),
            Medicamento = Medicamento,
            LocalAplicacao = LocalAplicacao,
            Data = Data,
            Hora = Hora,
            DetalhesAplicacao = DetalhesAplicacao,
            Observacoes = Observacoes,
            Status = Status
        };

        // Validação local de formulário
        var (valido, erro) = AgendamentoValidator.Validar(agendamento);
        if (!valido)
        {
            DestacarErrosFormulario(erro);
            await _notificacaoService.EnviarToastAsync(erro);
            return;
        }

        // Verifica conflitos antes do envio
        if (PossuiConflito)
        {
            await _notificacaoService.EnviarToastAsync("Impossível salvar. Corrija o conflito de horário.");
            return;
        }

        await ExecutarComLoadingAsync(async () =>
        {
            try
            {
                await _agendamentoService.SalvarAsync(agendamento);
                await _notificacaoService.EnviarToastAsync("Agendamento salvo com sucesso!");
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await _notificacaoService.EnviarToastAsync($"Erro ao salvar: {ex.Message}");
            }
        });
    }

    [RelayCommand]
    private async Task ExcluirAsync()
    {
        if (!ModoEdicao) return;

        bool confirmacao = await Shell.Current.DisplayAlert(
            "Confirmação", 
            "Deseja realmente excluir este agendamento?", 
            "Sim, Excluir", 
            "Cancelar");

        if (confirmacao)
        {
            await ExecutarComLoadingAsync(async () =>
            {
                var removido = await _agendamentoService.ExcluirAsync(_agendamentoId);
                if (removido)
                {
                    await _notificacaoService.EnviarToastAsync("Agendamento removido.");
                    await Shell.Current.GoToAsync("..");
                }
                else
                {
                    await _notificacaoService.EnviarToastAsync("Erro ao excluir agendamento.");
                }
            });
        }
    }

    [RelayCommand]
    private async Task CancelarAsync()
    {
        await Shell.Current.GoToAsync("..");
    }

    private void LimparErros()
    {
        ErroCliente = null;
        ErroAplicador = null;
        ErroMedicamento = null;
        ErroLocal = null;
        ErroData = null;
    }

    private void DestacarErrosFormulario(string erro)
    {
        if (erro.Contains("cliente")) ErroCliente = erro;
        else if (erro.Contains("aplicador")) ErroAplicador = erro;
        else if (erro.Contains("medicamento")) ErroMedicamento = erro;
        else if (erro.Contains("local")) ErroLocal = erro;
        else if (erro.Contains("passado")) ErroData = erro;
    }
}
