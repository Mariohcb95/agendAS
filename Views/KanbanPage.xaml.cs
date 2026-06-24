using AgendAS.Helpers;
using AgendAS.ViewModels;
using Microsoft.Maui.Controls;

namespace AgendAS.Views;

public partial class KanbanPage : ContentPage
{
    private readonly KanbanViewModel _viewModel;

    public KanbanPage() : this(ServicoLocalizador.Resolver<KanbanViewModel>()) { }

    public KanbanPage(KanbanViewModel viewModel)
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
