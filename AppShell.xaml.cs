using AgendAS.Views;
using Microsoft.Maui.Controls;

namespace AgendAS;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Registro de rotas que não estão listadas no menu principal do Shell
        Routing.RegisterRoute(nameof(NovoAgendamentoPage), typeof(NovoAgendamentoPage));

        // Força a página de login como a página padrão de inicialização no Shell
        CurrentItem = Items[0];
    }
}
