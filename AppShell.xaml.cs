using AgendAS.Views;
using Microsoft.Maui.Controls;

namespace AgendAS;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Registro de rotas para navegação programática (páginas que não estão no Flyout)
        Routing.RegisterRoute(nameof(NovoAgendamentoPage), typeof(NovoAgendamentoPage));
    }
}
