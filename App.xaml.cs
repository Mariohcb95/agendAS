using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace AgendAS;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        try
        {
            // Inicializar os recursos dinâmicos de cor com base no tema salvo
            var temaSalvo = Preferences.Default.Get("AppTemaSelecionado", "Padrao");
            AplicarTema(temaSalvo);
        }
        catch (System.Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[AgendAS] Erro ao inicializar tema no construtor do App: {ex.Message}");
        }
    }

    public void AplicarTema(string tema)
    {
        var recursos = Resources;
        if (recursos == null) return;

        bool usarDark = false;
        try
        {
            usarDark = tema == "Dark" || (tema == "Padrao" && RequestedTheme == AppTheme.Dark);
        }
        catch (System.Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[AgendAS] Erro ao ler RequestedTheme: {ex.Message}");
            usarDark = tema == "Dark"; // Fallback caso quebre no bootstrapping nativo
        }

        try
        {
            // Atualiza as cores dinâmicas no dicionário de recursos com base no tema selecionado
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

        // Altera o UserAppTheme na UI Thread de forma assíncrona para evitar quebras nativas do Android no OnCreate
        Dispatcher.Dispatch(() =>
        {
            try
            {
                UserAppTheme = tema switch
                {
                    "Dark" => AppTheme.Dark,
                    "Light" => AppTheme.Light,
                    _ => AppTheme.Unspecified
                };
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[AgendAS] Erro ao alterar UserAppTheme: {ex.Message}");
            }
        });
    }

    protected override void OnStart()
    {
        base.OnStart();

        // Monitorar alteração de tema do sistema operacional quando configurado como Padrão
        RequestedThemeChanged += (s, e) =>
        {
            var temaSalvo = Preferences.Default.Get("AppTemaSelecionado", "Padrao");
            if (temaSalvo == "Padrao")
            {
                AplicarTema("Padrao");
            }
        };
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShell());
    }
}