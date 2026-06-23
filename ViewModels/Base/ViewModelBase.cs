using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AgendAS.ViewModels.Base;

public partial class ViewModelBase : ObservableObject
{
    [ObservableProperty]
    private bool estaCarregando;

    [ObservableProperty]
    private bool estaVazio;

    [ObservableProperty]
    private string titulo = string.Empty;

    public virtual Task InicializarAsync() => Task.CompletedTask;

    protected async Task ExecutarComLoadingAsync(Func<Task> acao)
    {
        if (EstaCarregando) return;

        try
        {
            EstaCarregando = true;
            await acao();
        }
        finally
        {
            EstaCarregando = false;
        }
    }
}
