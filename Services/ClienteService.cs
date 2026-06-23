using System.Collections.Generic;
using System.Threading.Tasks;
using AgendAS.Models;
using AgendAS.Repositories.Interfaces;
using AgendAS.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace AgendAS.Services;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _clienteRepository;
    private readonly ILogger<ClienteService> _logger;

    public ClienteService(IClienteRepository clienteRepository, ILogger<ClienteService> logger)
    {
        _clienteRepository = clienteRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<Cliente>> BuscarTodosAsync()
    {
        _logger.LogInformation("Buscando todos os clientes");
        return await _clienteRepository.BuscarTodosAsync();
    }

    public async Task<Cliente?> BuscarPorIdAsync(int id)
    {
        _logger.LogInformation("Buscando cliente por Id: {Id}", id);
        return await _clienteRepository.BuscarPorIdAsync(id);
    }

    public async Task<IEnumerable<Cliente>> BuscarPorNomeAsync(string nome)
    {
        _logger.LogInformation("Buscando clientes por nome: {Nome}", nome);
        return await _clienteRepository.BuscarPorNomeAsync(nome);
    }

    public async Task<Cliente> SalvarAsync(Cliente cliente)
    {
        if (cliente.Id <= 0)
        {
            _logger.LogInformation("Adicionando novo cliente: {Nome}", cliente.Nome);
            return await _clienteRepository.AdicionarAsync(cliente);
        }
        else
        {
            _logger.LogInformation("Atualizando cliente: {Nome}", cliente.Nome);
            return await _clienteRepository.AtualizarAsync(cliente);
        }
    }

    public async Task<bool> ExcluirAsync(int id)
    {
        _logger.LogInformation("Excluindo cliente de Id: {Id}", id);
        return await _clienteRepository.RemoverAsync(id);
    }
}
