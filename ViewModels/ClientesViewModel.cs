using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AgendAS.Models;
using AgendAS.Services.Interfaces;
using AgendAS.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AgendAS.ViewModels;

public partial class ClientesViewModel : ViewModelBase
{
    private readonly IClienteService _clienteService;

    [ObservableProperty]
    private string textoPesquisa = string.Empty;

    public ObservableCollection<Cliente> Clientes { get; } = new();

    public ClientesViewModel(IClienteService clienteService)
    {
        _clienteService = clienteService;
        Titulo = "Clientes";
    }

    public override async Task InicializarAsync()
    {
        await CarregarClientesAsync();
    }

    partial void OnTextoPesquisaChanged(string value)
    {
        _ = CarregarClientesAsync();
    }

    [RelayCommand]
    public async Task CarregarClientesAsync()
    {
        await ExecutarComLoadingAsync(async () =>
        {
            var result = await _clienteService.BuscarPorNomeAsync(TextoPesquisa);
            
            Clientes.Clear();
            foreach (var c in result)
            {
                Clientes.Add(c);
            }

            EstaVazio = !Clientes.Any();
        });
    }
}
