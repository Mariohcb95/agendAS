using System.Linq;
using System.Threading.Tasks;
using AgendAS.Models;
using AgendAS.Repositories.Interfaces;

namespace AgendAS.Repositories.Mock;

public class UsuarioRepositoryMock : IUsuarioRepository
{
    public async Task<Usuario?> AutenticarAsync(string nomeUsuario, string senha)
    {
        await Task.Delay(500); // Autenticação demora um pouco mais
        return DadosMock.Usuarios.FirstOrDefault(u => 
            u.NomeUsuario.Equals(nomeUsuario, System.StringComparison.OrdinalIgnoreCase) && 
            u.Senha == senha);
    }

    public async Task<Usuario?> BuscarPorIdAsync(int id)
    {
        await Task.Delay(200);
        return DadosMock.Usuarios.FirstOrDefault(u => u.Id == id);
    }
}
