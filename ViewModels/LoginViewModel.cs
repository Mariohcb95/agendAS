using System;
using System.Threading.Tasks;
using AgendAS.Services.Interfaces;
using AgendAS.Validators;
using AgendAS.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;

namespace AgendAS.ViewModels;

public partial class LoginViewModel : ViewModelBase
{
    private readonly IUsuarioService _usuarioService;
    private readonly INotificacaoService _notificacaoService;

    [ObservableProperty]
    private string nomeUsuario = string.Empty;

    [ObservableProperty]
    private string senha = string.Empty;

    [ObservableProperty]
    private string? erroUsuario;

    [ObservableProperty]
    private string? erroSenha;

    [ObservableProperty]
    private string? erroLogin;

    public LoginViewModel(IUsuarioService usuarioService, INotificacaoService notificacaoService)
    {
        _usuarioService = usuarioService;
        _notificacaoService = notificacaoService;
        Titulo = "Autenticação";
    }

    [RelayCommand]
    private async Task EntrarAsync()
    {
        // Limpar mensagens de erro
        ErroUsuario = null;
        ErroSenha = null;
        ErroLogin = null;

        // Validação local
        var (valido, erro) = LoginValidator.Validar(NomeUsuario, Senha);
        if (!valido)
        {
            if (erro.Contains("usuário")) ErroUsuario = erro;
            else if (erro.Contains("senha")) ErroSenha = erro;
            return;
        }

        await ExecutarComLoadingAsync(async () =>
        {
            var autenticado = await _usuarioService.AutenticarAsync(NomeUsuario, Senha);
            if (autenticado)
            {
                var usuario = await _usuarioService.ObterUsuarioLogadoAsync();
                await _notificacaoService.EnviarToastAsync($"Seja bem-vindo, {usuario?.NomeCompleto}!");
                
                // Navega para o Dashboard no Shell limpando a pilha de Login
                await Shell.Current.GoToAsync("///DashboardPage");
            }
            else
            {
                ErroLogin = "Usuário ou senha inválidos. Tente novamente.";
                await _notificacaoService.EnviarToastAsync("Falha na autenticação.");
            }
        });
    }

    [RelayCommand]
    private async Task EsqueciSenhaAsync()
    {
        await _notificacaoService.EnviarToastAsync("Recuperação de senha: Entre em contato com a administração.");
    }
}
