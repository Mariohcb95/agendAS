using AgendAS.Helpers;
using AgendAS.ViewModels;
using Microsoft.Maui.Controls;

namespace AgendAS.Views;

public partial class PerfilPage : ContentPage
{
    private readonly PerfilViewModel _viewModel;

    public PerfilPage() : this(ServicoLocalizador.Resolver<PerfilViewModel>()) { }

    public PerfilPage(PerfilViewModel viewModel)
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
