using System.Collections.Generic;
using System.Threading.Tasks;
using AgendAS.Models;

namespace AgendAS.Repositories.Interfaces;

public interface IClienteRepository
{
    Task<IEnumerable<Cliente>> BuscarTodosAsync();
    Task<Cliente?> BuscarPorIdAsync(int id);
    Task<IEnumerable<Cliente>> BuscarPorNomeAsync(string nome);
    Task<Cliente> AdicionarAsync(Cliente cliente);
    Task<Cliente> AtualizarAsync(Cliente cliente);
    Task<bool> RemoverAsync(int id);
}
