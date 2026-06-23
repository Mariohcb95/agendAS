using AgendAS.ViewModels;
using Microsoft.Maui.Controls;

namespace AgendAS.Views;

public partial class AplicadoresPage : ContentPage
{
    private readonly AplicadoresViewModel _viewModel;

    public AplicadoresPage(AplicadoresViewModel viewModel)
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
