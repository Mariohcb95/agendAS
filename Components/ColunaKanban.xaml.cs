using System.Collections;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using AgendAS.Models;
using AgendAS.Models.Enums;

namespace AgendAS.Components;

public partial class ColunaKanban : ContentView
{
    public static readonly BindableProperty TituloProperty =
        BindableProperty.Create(nameof(Titulo), typeof(string), typeof(ColunaKanban), string.Empty);

    public static readonly BindableProperty ContadorProperty =
        BindableProperty.Create(nameof(Contador), typeof(int), typeof(ColunaKanban), 0);

    public static readonly BindableProperty CorColunaProperty =
        BindableProperty.Create(nameof(CorColuna), typeof(Color), typeof(ColunaKanban), Colors.Blue);

    public static readonly BindableProperty ListaAgendamentosProperty =
        BindableProperty.Create(nameof(ListaAgendamentos), typeof(IEnumerable), typeof(ColunaKanban), null);

    public static readonly BindableProperty StatusAlvoProperty =
        BindableProperty.Create(nameof(StatusAlvo), typeof(StatusAgendamento), typeof(ColunaKanban), StatusAgendamento.Agendado);

    public static readonly BindableProperty ComandoDropProperty =
        BindableProperty.Create(nameof(ComandoDrop), typeof(ICommand), typeof(ColunaKanban), null);

    public static readonly BindableProperty EmptyTemplateProperty =
        BindableProperty.Create(nameof(EmptyTemplate), typeof(DataTemplate), typeof(ColunaKanban), null);

    public string Titulo
    {
        get => (string)GetValue(TituloProperty);
        set => SetValue(TituloProperty, value);
    }

    public int Contador
    {
        get => (int)GetValue(ContadorProperty);
        set => SetValue(ContadorProperty, value);
    }

    public Color CorColuna
    {
        get => (Color)GetValue(CorColunaProperty);
        set => SetValue(CorColunaProperty, value);
    }

    public IEnumerable ListaAgendamentos
    {
        get => (IEnumerable)GetValue(ListaAgendamentosProperty);
        set => SetValue(ListaAgendamentosProperty, value);
    }

    public StatusAgendamento StatusAlvo
    {
        get => (StatusAgendamento)GetValue(StatusAlvoProperty);
        set => SetValue(StatusAlvoProperty, value);
    }

    public ICommand ComandoDrop
    {
        get => (ICommand)GetValue(ComandoDropProperty);
        set => SetValue(ComandoDropProperty, value);
    }

    public DataTemplate EmptyTemplate
    {
        get => (DataTemplate)GetValue(EmptyTemplateProperty);
        set => SetValue(EmptyTemplateProperty, value);
    }

    public ColunaKanban()
    {
        InitializeComponent();
    }

    private void OnDragOver(object sender, DragEventArgs e)
    {
        if (sender is Border border)
        {
            border.Opacity = 0.7;
            border.StrokeThickness = 2;
        }
    }

    private void OnDragLeave(object sender, DragEventArgs e)
    {
        RestaurarFeedbackVisual(sender);
    }

    private void OnDrop(object sender, DropEventArgs e)
    {
        RestaurarFeedbackVisual(sender);

        if (e.Data.Properties.TryGetValue("Agendamento", out var obj) && obj is Agendamento agendamento)
        {
            if (ComandoDrop != null && ComandoDrop.CanExecute(null))
            {
                var parametros = new DragDropParametros(agendamento, StatusAlvo);
                ComandoDrop.Execute(parametros);
            }
        }
    }

    private void RestaurarFeedbackVisual(object sender)
    {
        if (sender is Border border)
        {
            border.Opacity = 1.0;
            border.StrokeThickness = 1;
        }
    }
}
