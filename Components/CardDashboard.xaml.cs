using Microsoft.Maui.Controls;

namespace AgendAS.Components;

public partial class CardDashboard : ContentView
{
    public static readonly BindableProperty TituloProperty =
        BindableProperty.Create(nameof(Titulo), typeof(string), typeof(CardDashboard), string.Empty);

    public static readonly BindableProperty IconeProperty =
        BindableProperty.Create(nameof(Icone), typeof(string), typeof(CardDashboard), string.Empty);

    public static readonly BindableProperty ValorProperty =
        BindableProperty.Create(nameof(Valor), typeof(string), typeof(CardDashboard), string.Empty);

    public static readonly BindableProperty CorIconeProperty =
        BindableProperty.Create(nameof(CorIcone), typeof(Color), typeof(CardDashboard), Colors.Blue);

    public static readonly BindableProperty CorFundoIconeProperty =
        BindableProperty.Create(nameof(CorFundoIcone), typeof(Color), typeof(CardDashboard), Colors.LightBlue);

    public string Titulo
    {
        get => (string)GetValue(TituloProperty);
        set => SetValue(TituloProperty, value);
    }

    public string Icone
    {
        get => (string)GetValue(IconeProperty);
        set => SetValue(IconeProperty, value);
    }

    public string Valor
    {
        get => (string)GetValue(ValorProperty);
        set => SetValue(ValorProperty, value);
    }

    public Color CorIcone
    {
        get => (Color)GetValue(CorIconeProperty);
        set => SetValue(CorIconeProperty, value);
    }

    public Color CorFundoIcone
    {
        get => (Color)GetValue(CorFundoIconeProperty);
        set => SetValue(CorFundoIconeProperty, value);
    }

    public CardDashboard()
    {
        InitializeComponent();
    }
}
