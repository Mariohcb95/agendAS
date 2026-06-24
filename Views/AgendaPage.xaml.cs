using AgendAS.Helpers;
using AgendAS.ViewModels;
using Microsoft.Maui.Controls;

namespace AgendAS.Views;

public partial class AgendaPage : ContentPage
{
    private readonly AgendaViewModel _viewModel;

    public AgendaPage() : this(ServicoLocalizador.Resolver<AgendaViewModel>()) { }

    public AgendaPage(AgendaViewModel viewModel)
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
