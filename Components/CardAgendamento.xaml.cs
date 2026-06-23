using System.Windows.Input;
using Microsoft.Maui.Controls;
using AgendAS.Models;

namespace AgendAS.Components;

public partial class CardAgendamento : ContentView
{
    public static readonly BindableProperty AgendamentoProperty =
        BindableProperty.Create(nameof(Agendamento), typeof(Agendamento), typeof(CardAgendamento), null);

    public static readonly BindableProperty ComandoToqueProperty =
        BindableProperty.Create(nameof(ComandoToque), typeof(ICommand), typeof(CardAgendamento), null);

    public Agendamento Agendamento
    {
        get => (Agendamento)GetValue(AgendamentoProperty);
        set => SetValue(AgendamentoProperty, value);
    }

    public ICommand ComandoToque
    {
        get => (ICommand)GetValue(ComandoToqueProperty);
        set => SetValue(ComandoToqueProperty, value);
    }

    public CardAgendamento()
    {
        InitializeComponent();
    }
}
