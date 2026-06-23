using System.Threading.Tasks;
using AgendAS.Models;

namespace AgendAS.Services.Interfaces;

public interface IUsuarioService
{
    Task<bool> AutenticarAsync(string nomeUsuario, string senha);
    Task<Usuario?> ObterUsuarioLogadoAsync();
    Task LogoutAsync();
}
