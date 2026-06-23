using System.Collections.Generic;
using System.Threading.Tasks;
using AgendAS.Models;

namespace AgendAS.Services.Interfaces;

public interface IClienteService
{
    Task<IEnumerable<Cliente>> BuscarTodosAsync();
    Task<Cliente?> BuscarPorIdAsync(int id);
    Task<IEnumerable<Cliente>> BuscarPorNomeAsync(string nome);
    Task<Cliente> SalvarAsync(Cliente cliente);
    Task<bool> ExcluirAsync(int id);
}
