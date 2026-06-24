using System.Threading.Tasks;
using AgendAS.Models;
using AgendAS.Services.Interfaces;
using AgendAS.ViewModels.Base;
using AgendAS.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;

namespace AgendAS.ViewModels;

public partial class PerfilViewModel : ViewModelBase
{
    private readonly IUsuarioService _usuarioService;
    private readonly INotificacaoService _notificacaoService;
    private readonly IServiceProvider _serviceProvider;

    [ObservableProperty]
    private Usuario? usuario;

    public PerfilViewModel(
        IUsuarioService usuarioService, 
        INotificacaoService notificacaoService,
        IServiceProvider serviceProvider)
    {
        _usuarioService = usuarioService;
        _notificacaoService = notificacaoService;
        _serviceProvider = serviceProvider;
        Titulo = "Meu Perfil";
    }

    public override async Task InicializarAsync()
    {
        Usuario = await _usuarioService.ObterUsuarioLogadoAsync();
    }

    [RelayCommand]
    private async Task LogoutAsync()
    {
        bool confirmacao = await Shell.Current.DisplayAlert(
            "Sair do App", 
            "Tem certeza que deseja encerrar a sessão?", 
            "Sair", 
            "Cancelar");

        if (confirmacao)
        {
            await _usuarioService.LogoutAsync();
            await _notificacaoService.EnviarToastAsync("Sessão encerrada.");

            // Volta para a LoginPage resolvida do DI, saindo do Shell
            var loginPage = _serviceProvider.GetRequiredService<LoginPage>();
            if (Application.Current != null)
            {
                Application.Current.MainPage = new NavigationPage(loginPage);
            }
        }
    }
}

