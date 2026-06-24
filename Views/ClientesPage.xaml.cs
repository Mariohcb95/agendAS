using AgendAS.Helpers;
using AgendAS.ViewModels;
using Microsoft.Maui.Controls;

namespace AgendAS.Views;

public partial class ClientesPage : ContentPage
{
    private readonly ClientesViewModel _viewModel;

    public ClientesPage() : this(ServicoLocalizador.Resolver<ClientesViewModel>()) { }

    public ClientesPage(ClientesViewModel viewModel)
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
