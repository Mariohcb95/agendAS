using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgendAS.Models;
using AgendAS.Repositories.Interfaces;

namespace AgendAS.Repositories.Mock;

public class ClienteRepositoryMock : IClienteRepository
{
    public async Task<IEnumerable<Cliente>> BuscarTodosAsync()
    {
        await Task.Delay(300); // Simulando latência
        return DadosMock.Clientes.OrderBy(c => c.Nome);
    }

    public async Task<Cliente?> BuscarPorIdAsync(int id)
    {
        await Task.Delay(200);
        return DadosMock.Clientes.FirstOrDefault(c => c.Id == id);
    }

    public async Task<IEnumerable<Cliente>> BuscarPorNomeAsync(string nome)
    {
        await Task.Delay(300);
        if (string.IsNullOrWhiteSpace(nome)) return DadosMock.Clientes;
        return DadosMock.Clientes.Where(c => c.Nome.Contains(nome, System.StringComparison.OrdinalIgnoreCase));
    }

    public async Task<Cliente> AdicionarAsync(Cliente cliente)
    {
        await Task.Delay(400);
        var novoId = DadosMock.Clientes.Any() ? DadosMock.Clientes.Max(c => c.Id) + 1 : 1;
        cliente.Id = novoId;
        DadosMock.Clientes.Add(cliente);
        return cliente;
    }

    public async Task<Cliente> AtualizarAsync(Cliente cliente)
    {
        await Task.Delay(400);
        var index = DadosMock.Clientes.FindIndex(c => c.Id == cliente.Id);
        if (index != -1)
        {
            DadosMock.Clientes[index] = cliente;
        }
        return cliente;
    }

    public async Task<bool> RemoverAsync(int id)
    {
        await Task.Delay(400);
        var cliente = DadosMock.Clientes.FirstOrDefault(c => c.Id == id);
        if (cliente != null)
        {
            DadosMock.Clientes.Remove(cliente);
            return true;
        }
        return false;
    }
}
