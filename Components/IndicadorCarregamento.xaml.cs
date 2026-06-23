using Microsoft.Maui.Controls;

namespace AgendAS.Components;

public partial class IndicadorCarregamento : ContentView
{
    public static readonly BindableProperty EstaCarregandoProperty =
        BindableProperty.Create(nameof(EstaCarregando), typeof(bool), typeof(IndicadorCarregamento), false);

    public static readonly BindableProperty TextoProperty =
        BindableProperty.Create(nameof(Texto), typeof(string), typeof(IndicadorCarregamento), "Carregando...");

    public bool EstaCarregando
    {
        get => (bool)GetValue(EstaCarregandoProperty);
        set => SetValue(EstaCarregandoProperty, value);
    }

    public string Texto
    {
        get => (string)GetValue(TextoProperty);
        set => SetValue(TextoProperty, value);
    }

    public IndicadorCarregamento()
    {
        InitializeComponent();
    }
}
