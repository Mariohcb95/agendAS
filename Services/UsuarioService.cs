using System.Threading.Tasks;
using AgendAS.Models;
using AgendAS.Repositories.Interfaces;
using AgendAS.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Storage;

namespace AgendAS.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ILogger<UsuarioService> _logger;
    private Usuario? _usuarioLogado;

    public UsuarioService(IUsuarioRepository usuarioRepository, ILogger<UsuarioService> logger)
    {
        _usuarioRepository = usuarioRepository;
        _logger = logger;
    }

    public async Task<bool> AutenticarAsync(string nomeUsuario, string senha)
    {
        _logger.LogInformation("Tentativa de login para usuário: {Usuario}", nomeUsuario);
        var usuario = await _usuarioRepository.AutenticarAsync(nomeUsuario, senha);
        
        if (usuario != null)
        {
            _usuarioLogado = usuario;
            Preferences.Default.Set("UsuarioAutenticado", true);
            Preferences.Default.Set("UsuarioIdLogado", usuario.Id);
            Preferences.Default.Set("UsuarioNomeLogado", usuario.NomeCompleto);
            _logger.LogInformation("Login bem sucedido para: {NomeCompleto}", usuario.NomeCompleto);
            return true;
        }

        _logger.LogWarning("Falha na autenticação para usuário: {Usuario}", nomeUsuario);
        return false;
    }

    public async Task<Usuario?> ObterUsuarioLogadoAsync()
    {
        if (_usuarioLogado != null) return _usuarioLogado;

        var idLogado = Preferences.Default.Get("UsuarioIdLogado", -1);
        if (idLogado != -1)
        {
            _usuarioLogado = await _usuarioRepository.BuscarPorIdAsync(idLogado);
            return _usuarioLogado;
        }

        return null;
    }

    public Task LogoutAsync()
    {
        _logger.LogInformation("Realizando logout do usuário");
        _usuarioLogado = null;
        Preferences.Default.Remove("UsuarioAutenticado");
        Preferences.Default.Remove("UsuarioIdLogado");
        Preferences.Default.Remove("UsuarioNomeLogado");
        return Task.CompletedTask;
    }
}
