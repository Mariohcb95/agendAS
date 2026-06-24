using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using AgendAS.Helpers;

namespace AgendAS;

public partial class App : Application
{
    private readonly IServiceProvider _serviceProvider;

    public App(IServiceProvider serviceProvider)
    {
        try
        {
            _serviceProvider = serviceProvider;
            ServicoLocalizador.Inicializar(serviceProvider);

            InitializeComponent();

            var loginPage = _serviceProvider.GetRequiredService<Views.LoginPage>();
            MainPage = new NavigationPage(loginPage);
        }
        catch (System.Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[AgendAS] ERRO CRÍTICO NA INICIALIZAÇÃO: {ex}");
            System.Console.WriteLine($"[AgendAS] ERRO CRÍTICO NA INICIALIZAÇÃO: {ex}");
            
            // Exibe o erro diretamente na tela no emulador para podermos ler o stack trace gerenciado
            MainPage = new ContentPage
            {
                Content = new ScrollView
                {
                    Content = new Label
                    {
                        Text = $"Erro de Inicialização:\n\n{ex.Message}\n\nDetalhes:\n{ex}",
                        TextColor = Microsoft.Maui.Graphics.Colors.Red,
                        FontFamily = "monospace",
                        Padding = 20
                    }
                }
            };
        }
    }
  private string _ultimoTemaAplicado = string.Empty;

    public void AplicarTema(string tema)
    {
        // Se já aplicamos este tema recentemente, não faz nada (evita loops e reentrâncias!)
        // Se for "Padrao", avaliamos se o tema efetivo do SO mudou em relação ao último aplicado
        var temaEfetivo = tema;
        if (tema == "Padrao")
        {
            try
            {
                temaEfetivo = RequestedTheme == AppTheme.Dark ? "Dark" : "Light";
            }
            catch
            {
                temaEfetivo = "Light"; // Fallback
            }
        }

        if (_ultimoTemaAplicado == temaEfetivo)
        {
            // O tema e as cores do dicionário já estão corretas, não faz nada!
            // Mas ainda garante que o UserAppTheme está correto de forma segura
            AtualizarUserAppTheme(tema);
            return;
        }

        _ultimoTemaAplicado = temaEfetivo;

        var recursos = Resources;
        if (recursos != null)
        {
            bool usarDark = temaEfetivo == "Dark";
            try
            {
                recursos["FundoCor"] = usarDark ? recursos["FundoEscuro"] : recursos["FundoClaro"];
                recursos["CardFundoCor"] = usarDark ? recursos["CardFundoEscuro"] : recursos["CardFundoClaro"];
                recursos["TextoPrimarioCor"] = usarDark ? recursos["TextoPrimarioEscuro"] : recursos["TextoPrimarioClaro"];
                recursos["TextoSecundarioCor"] = usarDark ? recursos["TextoSecundarioEscuro"] : recursos["TextoSecundarioClaro"];
                recursos["TextoTerciarioCor"] = usarDark ? recursos["TextoTerciarioEscuro"] : recursos["TextoTerciarioClaro"];
                recursos["BordaCor"] = usarDark ? recursos["BordaEscura"] : recursos["BordaClara"];
                recursos["FundoFormularioCor"] = usarDark ? recursos["FundoFormularioEscuro"] : recursos["FundoFormularioClaro"];
                recursos["SombraCor"] = usarDark ? recursos["SombraCorEscura"] : recursos["SombraCorClara"];
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[AgendAS] Erro ao atualizar dicionário de cores: {ex.Message}");
            }
        }

        AtualizarUserAppTheme(tema);
    }

    private void AtualizarUserAppTheme(string tema)
    {
        var novoAppTheme = tema switch
        {
            "Dark" => AppTheme.Dark,
            "Light" => AppTheme.Light,
            _ => AppTheme.Unspecified
        };

        if (UserAppTheme != novoAppTheme)
        {
            Dispatcher.Dispatch(() =>
            {
                try
                {
                    if (UserAppTheme != novoAppTheme)
                    {
                        UserAppTheme = novoAppTheme;
                    }
                }
                catch (System.Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"[AgendAS] Erro ao alterar UserAppTheme: {ex.Message}");
                }
            });
        }
    }

    protected override void OnStart()
    {
        base.OnStart();

        try
        {
            // Inicializar os recursos dinâmicos de cor com base no tema salvo após a inicialização nativa estar concluída
            var temaSalvo = Preferences.Default.Get("AppTemaSelecionado", "Padrao");
            AplicarTema(temaSalvo);
        }
        catch (System.Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[AgendAS] Erro ao inicializar tema no OnStart: {ex.Message}");
        }

        RequestedThemeChanged += (s, e) =>
        {
            var temaSalvo = Preferences.Default.Get("AppTemaSelecionado", "Padrao");
            if (temaSalvo == "Padrao")
            {
                // Dispara no Dispatcher de forma assíncrona para que a alteração nativa seja consolidada no SO
                Dispatcher.Dispatch(() =>
                {
                    AplicarTema("Padrao");
                });
            }
        };
    }
}
