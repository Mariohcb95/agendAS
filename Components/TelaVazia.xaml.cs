using Microsoft.Maui.Controls;

namespace AgendAS.Components;

public partial class TelaVazia : ContentView
{
    public static readonly BindableProperty TituloProperty =
        BindableProperty.Create(nameof(Titulo), typeof(string), typeof(TelaVazia), "Nenhum dado encontrado");

    public static readonly BindableProperty DescricaoProperty =
        BindableProperty.Create(nameof(Descricao), typeof(string), typeof(TelaVazia), "Não há registros cadastrados no momento.");

    public static readonly BindableProperty IconeProperty =
        BindableProperty.Create(nameof(Icone), typeof(string), typeof(TelaVazia), "\ue99a");

    public string Titulo
    {
        get => (string)GetValue(TituloProperty);
        set => SetValue(TituloProperty, value);
    }

    public string Descricao
    {
        get => (string)GetValue(DescricaoProperty);
        set => SetValue(DescricaoProperty, value);
    }

    public string Icone
    {
        get => (string)GetValue(IconeProperty);
        set => SetValue(IconeProperty, value);
    }

    public TelaVazia()
    {
        InitializeComponent();
    }
}
