using AgendAS.Helpers;
using AgendAS.ViewModels;
using Microsoft.Maui.Controls;

namespace AgendAS.Views;

public partial class ConfiguracoesPage : ContentPage
{
    public ConfiguracoesPage() : this(ServicoLocalizador.Resolver<ConfiguracoesViewModel>()) { }

    public ConfiguracoesPage(ConfiguracoesViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
