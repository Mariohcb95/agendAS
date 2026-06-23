using System.Threading.Tasks;
using AgendAS.Models;

namespace AgendAS.Repositories.Interfaces;

public interface IUsuarioRepository
{
    Task<Usuario?> AutenticarAsync(string nomeUsuario, string senha);
    Task<Usuario?> BuscarPorIdAsync(int id);
}
