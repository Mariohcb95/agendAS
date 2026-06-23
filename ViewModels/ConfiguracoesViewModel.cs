using System;
using System.Threading.Tasks;
using AgendAS.Services.Interfaces;
using AgendAS.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace AgendAS.ViewModels;

public partial class ConfiguracoesViewModel : ViewModelBase
{
    private readonly INotificacaoService _notificacaoService;

    [ObservableProperty]
    private bool temaEscuroAtivo;

    [ObservableProperty]
    private bool notificacoesAtivas;

    public ConfiguracoesViewModel(INotificacaoService notificacaoService)
    {
        _notificacaoService = notificacaoService;
        Titulo = "Configurações";

        // Inicializar com base nos Preferences salvos
        notificacoesAtivas = Preferences.Default.Get("NotificacoesAtivas", true);
        
        // Verifica se o tema atual está definido como Dark
        var temaSalvo = Preferences.Default.Get("AppTemaSelecionado", "Padrao");
        if (temaSalvo == "Dark")
        {
            temaEscuroAtivo = true;
        }
        else if (temaSalvo == "Light")
        {
            temaEscuroAtivo = false;
        }
        else
        {
            // Padrão do sistema
            temaEscuroAtivo = Application.Current?.RequestedTheme == AppTheme.Dark;
        }
    }

    // Monitora a mudança do Switch de Dark Mode na UI e aplica
    partial void OnTemaEscuroAtivoChanged(bool value)
    {
        if (Application.Current is App app)
        {
            string tema = value ? "Dark" : "Light";
            Preferences.Default.Set("AppTemaSelecionado", tema);
            app.AplicarTema(tema);
            _ = _notificacaoService.EnviarToastAsync(value ? "Tema Escuro premium ativado" : "Tema Claro premium ativado");
        }
    }

    // Monitora a mudança do Switch de Notificações
    partial void OnNotificacoesAtivasChanged(bool value)
    {
        Preferences.Default.Set("NotificacoesAtivas", value);
        if (value)
        {
            _ = _notificacaoService.EnviarToastAsync("Lembretes de aplicação ativados");
        }
        else
        {
            _ = _notificacaoService.EnviarToastAsync("Lembretes desativados");
        }
    }
}
