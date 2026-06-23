using Microsoft.Maui.Controls;

namespace AgendAS.Components;

[ContentProperty(nameof(Conteudo))]
public partial class CampoFormulario : ContentView
{
    public static readonly BindableProperty TituloProperty =
        BindableProperty.Create(nameof(Titulo), typeof(string), typeof(CampoFormulario), string.Empty);

    public static readonly BindableProperty MensagemErroProperty =
        BindableProperty.Create(nameof(MensagemErro), typeof(string), typeof(CampoFormulario), string.Empty,
            propertyChanged: OnMensagemErroChanged);

    public static readonly BindableProperty TemErroProperty =
        BindableProperty.Create(nameof(TemErro), typeof(bool), typeof(CampoFormulario), false);

    public static readonly BindableProperty AlturaCampoProperty =
        BindableProperty.Create(nameof(AlturaCampo), typeof(double), typeof(CampoFormulario), 50.0);

    public static readonly BindableProperty ConteudoProperty =
        BindableProperty.Create(nameof(Conteudo), typeof(View), typeof(CampoFormulario), null,
            propertyChanged: OnConteudoChanged);

    public string Titulo
    {
        get => (string)GetValue(TituloProperty);
        set => SetValue(TituloProperty, value);
    }

    public string MensagemErro
    {
        get => (string)GetValue(MensagemErroProperty);
        set => SetValue(MensagemErroProperty, value);
    }

    public bool TemErro
    {
        get => (bool)GetValue(TemErroProperty);
        set => SetValue(TemErroProperty, value);
    }

    public double AlturaCampo
    {
        get => (double)GetValue(AlturaCampoProperty);
        set => SetValue(AlturaCampoProperty, value);
    }

    public View Conteudo
    {
        get => (View)GetValue(ConteudoProperty);
        set => SetValue(ConteudoProperty, value);
    }

    public CampoFormulario()
    {
        InitializeComponent();
        
        // Se o conteúdo já foi definido antes do InitializeComponent concluir, aplica ao Presenter agora
        if (Conteudo != null && Presenter != null)
        {
            Presenter.Content = Conteudo;
        }
    }

    private static void OnMensagemErroChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is CampoFormulario campo)
        {
            campo.TemErro = !string.IsNullOrWhiteSpace(newValue as string);
        }
    }

    private static void OnConteudoChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is CampoFormulario campo && campo.Presenter != null)
        {
            campo.Presenter.Content = (View)newValue;
        }
    }
}
