using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace AgendAS.Components;

public partial class BotaoPrimario : ContentView
{
    public static readonly BindableProperty TextoProperty =
        BindableProperty.Create(nameof(Texto), typeof(string), typeof(BotaoPrimario), string.Empty);

    public static readonly BindableProperty ComandoProperty =
        BindableProperty.Create(nameof(Comando), typeof(ICommand), typeof(BotaoPrimario), null);

    public static readonly BindableProperty EstaCarregandoProperty =
        BindableProperty.Create(nameof(EstaCarregando), typeof(bool), typeof(BotaoPrimario), false);

    public string Texto
    {
        get => (string)GetValue(TextoProperty);
        set => SetValue(TextoProperty, value);
    }

    public ICommand Comando
    {
        get => (ICommand)GetValue(ComandoProperty);
        set => SetValue(ComandoProperty, value);
    }

    public bool EstaCarregando
    {
        get => (bool)GetValue(EstaCarregandoProperty);
        set => SetValue(EstaCarregandoProperty, value);
    }

    public BotaoPrimario()
    {
        InitializeComponent();
    }
}
