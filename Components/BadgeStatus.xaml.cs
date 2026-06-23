using Microsoft.Maui.Controls;
using AgendAS.Models.Enums;

namespace AgendAS.Components;

public partial class BadgeStatus : ContentView
{
    public static readonly BindableProperty StatusProperty =
        BindableProperty.Create(nameof(Status), typeof(StatusAgendamento), typeof(BadgeStatus), StatusAgendamento.Agendado);

    public StatusAgendamento Status
    {
        get => (StatusAgendamento)GetValue(StatusProperty);
        set => SetValue(StatusProperty, value);
    }

    public BadgeStatus()
    {
        InitializeComponent();
    }
}
