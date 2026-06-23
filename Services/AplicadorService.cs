using System.Collections.Generic;
using System.Threading.Tasks;
using AgendAS.Models;
using AgendAS.Repositories.Interfaces;
using AgendAS.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace AgendAS.Services;

public class AplicadorService : IAplicadorService
{
    private readonly IAplicadorRepository _aplicadorRepository;
    private readonly ILogger<AplicadorService> _logger;

    public AplicadorService(IAplicadorRepository aplicadorRepository, ILogger<AplicadorService> logger)
    {
        _aplicadorRepository = aplicadorRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<Aplicador>> BuscarTodosAsync()
    {
        _logger.LogInformation("Buscando todos os aplicadores");
        return await _aplicadorRepository.BuscarTodosAsync();
    }

    public async Task<Aplicador?> BuscarPorIdAsync(int id)
    {
        _logger.LogInformation("Buscando aplicador por Id: {Id}", id);
        return await _aplicadorRepository.BuscarPorIdAsync(id);
    }

    public async Task<IEnumerable<Aplicador>> BuscarDisponiveisAsync()
    {
        _logger.LogInformation("Buscando aplicadores disponíveis");
        return await _aplicadorRepository.BuscarDisponiveisAsync();
    }

    public async Task<Aplicador> SalvarAsync(Aplicador aplicador)
    {
        if (aplicador.Id <= 0)
        {
            _logger.LogInformation("Adicionando novo aplicador: {Nome}", aplicador.Nome);
            return await _aplicadorRepository.AdicionarAsync(aplicador);
        }
        else
        {
            _logger.LogInformation("Atualizando aplicador: {Nome}", aplicador.Nome);
            return await _aplicadorRepository.AtualizarAsync(aplicador);
        }
    }

    public async Task<bool> ExcluirAsync(int id)
    {
        _logger.LogInformation("Excluindo aplicador de Id: {Id}", id);
        return await _aplicadorRepository.RemoverAsync(id);
    }
}
