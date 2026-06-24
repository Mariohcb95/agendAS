using AgendAS.Helpers;
using AgendAS.ViewModels;
using Microsoft.Maui.Controls;

namespace AgendAS.Views;

public partial class DashboardPage : ContentPage
{
    private readonly DashboardViewModel _viewModel;

    /// <summary>
    /// Construtor sem parâmetros para DataTemplate do Shell.
    /// Resolve o ViewModel do DI container automaticamente.
    /// </summary>
    public DashboardPage() : this(ServicoLocalizador.Resolver<DashboardViewModel>()) { }

    public DashboardPage(DashboardViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.InicializarAsync();
    }
}
