using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AgendAS.Models;
using AgendAS.Services.Interfaces;
using AgendAS.ViewModels.Base;
using CommunityToolkit.Mvvm.Input;

namespace AgendAS.ViewModels;

public partial class AplicadoresViewModel : ViewModelBase
{
    private readonly IAplicadorService _aplicadorService;

    public ObservableCollection<Aplicador> Aplicadores { get; } = new();

    public AplicadoresViewModel(IAplicadorService aplicadorService)
    {
        _aplicadorService = aplicadorService;
        Titulo = "Aplicadores";
    }

    public override async Task InicializarAsync()
    {
        await CarregarAplicadoresAsync();
    }

    [RelayCommand]
    public async Task CarregarAplicadoresAsync()
    {
        await ExecutarComLoadingAsync(async () =>
        {
            var result = await _aplicadorService.BuscarTodosAsync();

            Aplicadores.Clear();
            foreach (var a in result)
            {
                Aplicadores.Add(a);
            }

            EstaVazio = !Aplicadores.Any();
        });
    }
}
