using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using AgendAS.Repositories.Interfaces;
using AgendAS.Repositories.Mock;
using AgendAS.Services.Interfaces;
using AgendAS.Services;
using AgendAS.ViewModels;
using AgendAS.Views;

namespace AgendAS;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit() // Registro do Community Toolkit
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("MaterialIcons-Regular.ttf", "MaterialIcons"); // Registro da fonte de ícones
            });

        // 1. Registro de Repositories (Singleton para manter os dados mockados na memória do App)
        builder.Services.AddSingleton<IUsuarioRepository, UsuarioRepositoryMock>();
        builder.Services.AddSingleton<IClienteRepository, ClienteRepositoryMock>();
        builder.Services.AddSingleton<IAplicadorRepository, AplicadorRepositoryMock>();
        builder.Services.AddSingleton<IAgendamentoRepository, AgendamentoRepositoryMock>();

        // 2. Registro de Serviços
        builder.Services.AddSingleton<IUsuarioService, UsuarioService>();
        builder.Services.AddSingleton<IClienteService, ClienteService>();
        builder.Services.AddSingleton<IAplicadorService, AplicadorService>();
        builder.Services.AddSingleton<IAgendamentoService, AgendamentoService>();
        builder.Services.AddSingleton<INotificacaoService, NotificacaoService>();

        // 3. Registro de ViewModels
        builder.Services.AddSingleton<LoginViewModel>();
        builder.Services.AddSingleton<DashboardViewModel>();
        builder.Services.AddSingleton<AgendaViewModel>();
        builder.Services.AddSingleton<KanbanViewModel>();
        builder.Services.AddSingleton<ClientesViewModel>();
        builder.Services.AddSingleton<AplicadoresViewModel>();
        builder.Services.AddSingleton<PerfilViewModel>();
        builder.Services.AddSingleton<ConfiguracoesViewModel>();
        
        // Transient para a tela de Novo/Editar Agendamento (re-instanciada a cada navegação)
        builder.Services.AddTransient<NovoAgendamentoViewModel>();

        // 4. Registro de Views
        builder.Services.AddSingleton<LoginPage>();
        builder.Services.AddSingleton<DashboardPage>();
        builder.Services.AddSingleton<AgendaPage>();
        builder.Services.AddSingleton<KanbanPage>();
        builder.Services.AddSingleton<ClientesPage>();
        builder.Services.AddSingleton<AplicadoresPage>();
        builder.Services.AddSingleton<PerfilPage>();
        builder.Services.AddSingleton<ConfiguracoesPage>();
        
        builder.Services.AddTransient<NovoAgendamentoPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
