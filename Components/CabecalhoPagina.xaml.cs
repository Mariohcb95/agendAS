using Microsoft.Maui.Controls;

namespace AgendAS.Components;

public partial class CabecalhoPagina : ContentView
{
    public static readonly BindableProperty TituloProperty =
        BindableProperty.Create(nameof(Titulo), typeof(string), typeof(CabecalhoPagina), string.Empty);

    public static readonly BindableProperty SubtituloProperty =
        BindableProperty.Create(nameof(Subtitulo), typeof(string), typeof(CabecalhoPagina), string.Empty);

    public string Titulo
    {
        get => (string)GetValue(TituloProperty);
        set => SetValue(TituloProperty, value);
    }

    public string Subtitulo
    {
        get => (string)GetValue(SubtituloProperty);
        set => SetValue(SubtituloProperty, value);
    }

    public CabecalhoPagina()
    {
        InitializeComponent();
    }
}
