using System;
using Microsoft.Maui.Controls;
using AgendAS.Models;

namespace AgendAS.Components;

public partial class CardKanban : ContentView
{
    public static readonly BindableProperty AgendamentoProperty =
        BindableProperty.Create(nameof(Agendamento), typeof(Agendamento), typeof(CardKanban), null);

    public Agendamento Agendamento
    {
        get => (Agendamento)GetValue(AgendamentoProperty);
        set => SetValue(AgendamentoProperty, value);
    }

    public CardKanban()
    {
        InitializeComponent();
    }

    private void OnDragStarting(object sender, DragStartingEventArgs e)
    {
        if (Agendamento != null)
        {
            e.Data.Properties["Agendamento"] = Agendamento;
            e.Data.Text = Agendamento.Id.ToString();
        }
    }
}
