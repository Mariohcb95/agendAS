using System.Threading.Tasks;
using AgendAS.Helpers;
using AgendAS.ViewModels;
using Microsoft.Maui.Controls;

namespace AgendAS.Views;

public partial class LoginPage : ContentPage
{
    private readonly LoginViewModel _viewModel;

    public LoginPage() : this(ServicoLocalizador.Resolver<LoginViewModel>()) { }

    public LoginPage(LoginViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Limpa campos ao retornar à tela de login
        _viewModel.NomeUsuario = string.Empty;
        _viewModel.Senha = string.Empty;
        _viewModel.ErroUsuario = null;
        _viewModel.ErroSenha = null;
        _viewModel.ErroLogin = null;

        // Configura estado inicial invisível do Card antes de renderizar
        LoginCard.TranslationY = 200;
        LoginCard.Opacity = 0;

        // Dispara a animação na thread de UI sem bloquear o ciclo OnAppearing principal da página
        Dispatcher.Dispatch(async () =>
        {
            await Task.Delay(100);
            try
            {
                await Task.WhenAll(
                    LoginCard.FadeTo(1, 500, Easing.CubicOut),
                    LoginCard.TranslateTo(0, 0, 500, Easing.CubicOut)
                );
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[AgendAS] Erro ao executar animação da LoginPage: {ex.Message}");
                
                // Fallback imediato: garante exibição do card se a animação nativa falhar ou estiver desativada no SO
                LoginCard.Opacity = 1;
                LoginCard.TranslationY = 0;
            }
        });
    }
}
