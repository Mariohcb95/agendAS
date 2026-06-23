using System.Collections.Generic;
using System.Threading.Tasks;
using AgendAS.Models;

namespace AgendAS.Repositories.Interfaces;

public interface IAplicadorRepository
{
    Task<IEnumerable<Aplicador>> BuscarTodosAsync();
    Task<Aplicador?> BuscarPorIdAsync(int id);
    Task<IEnumerable<Aplicador>> BuscarDisponiveisAsync();
    Task<Aplicador> AdicionarAsync(Aplicador aplicador);
    Task<Aplicador> AtualizarAsync(Aplicador aplicador);
    Task<bool> RemoverAsync(int id);
}
