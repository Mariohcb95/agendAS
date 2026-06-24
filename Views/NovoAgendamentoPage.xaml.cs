using AgendAS.Helpers;
using AgendAS.ViewModels;
using Microsoft.Maui.Controls;

namespace AgendAS.Views;

public partial class NovoAgendamentoPage : ContentPage
{
    private readonly NovoAgendamentoViewModel _viewModel;

    public NovoAgendamentoPage() : this(ServicoLocalizador.Resolver<NovoAgendamentoViewModel>()) { }

    public NovoAgendamentoPage(NovoAgendamentoViewModel viewModel)
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
