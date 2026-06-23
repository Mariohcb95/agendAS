using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgendAS.Models;
using AgendAS.Repositories.Interfaces;

namespace AgendAS.Repositories.Mock;

public class AplicadorRepositoryMock : IAplicadorRepository
{
    public async Task<IEnumerable<Aplicador>> BuscarTodosAsync()
    {
        await Task.Delay(300);
        return DadosMock.Aplicadores.OrderBy(a => a.Nome);
    }

    public async Task<Aplicador?> BuscarPorIdAsync(int id)
    {
        await Task.Delay(200);
        return DadosMock.Aplicadores.FirstOrDefault(a => a.Id == id);
    }

    public async Task<IEnumerable<Aplicador>> BuscarDisponiveisAsync()
    {
        await Task.Delay(300);
        return DadosMock.Aplicadores.Where(a => a.StatusDisponibilidade == Models.Enums.StatusDisponibilidade.Disponivel);
    }

    public async Task<Aplicador> AdicionarAsync(Aplicador aplicador)
    {
        await Task.Delay(400);
        var novoId = DadosMock.Aplicadores.Any() ? DadosMock.Aplicadores.Max(a => a.Id) + 1 : 1;
        aplicador.Id = novoId;
        DadosMock.Aplicadores.Add(aplicador);
        return aplicador;
    }

    public async Task<Aplicador> AtualizarAsync(Aplicador aplicador)
    {
        await Task.Delay(400);
        var index = DadosMock.Aplicadores.FindIndex(a => a.Id == aplicador.Id);
        if (index != -1)
        {
            DadosMock.Aplicadores[index] = aplicador;
        }
        return aplicador;
    }

    public async Task<bool> RemoverAsync(int id)
    {
        await Task.Delay(400);
        var aplicador = DadosMock.Aplicadores.FirstOrDefault(a => a.Id == id);
        if (aplicador != null)
        {
            DadosMock.Aplicadores.Remove(aplicador);
            return true;
        }
        return false;
    }
}
