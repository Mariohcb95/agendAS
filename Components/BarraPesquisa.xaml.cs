using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace AgendAS.Components;

public partial class BarraPesquisa : ContentView
{
    public static readonly BindableProperty TextoPesquisaProperty =
        BindableProperty.Create(nameof(TextoPesquisa), typeof(string), typeof(BarraPesquisa), string.Empty, BindingMode.TwoWay);

    public static readonly BindableProperty PlaceholderProperty =
        BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(BarraPesquisa), "Pesquisar...");

    public string TextoPesquisa
    {
        get => (string)GetValue(TextoPesquisaProperty);
        set => SetValue(TextoPesquisaProperty, value);
    }

    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    public ICommand ComandoLimpar { get; }

    public BarraPesquisa()
    {
        ComandoLimpar = new Command(() => TextoPesquisa = string.Empty);
        InitializeComponent();
    }
}
