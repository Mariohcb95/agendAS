using System.Collections.Generic;
using System.Threading.Tasks;
using AgendAS.Models;

namespace AgendAS.Services.Interfaces;

public interface IAplicadorService
{
    Task<IEnumerable<Aplicador>> BuscarTodosAsync();
    Task<Aplicador?> BuscarPorIdAsync(int id);
    Task<IEnumerable<Aplicador>> BuscarDisponiveisAsync();
    Task<Aplicador> SalvarAsync(Aplicador aplicador);
    Task<bool> ExcluirAsync(int id);
}
