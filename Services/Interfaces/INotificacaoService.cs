using System.Threading.Tasks;
using AgendAS.Models;

namespace AgendAS.Services.Interfaces;

public interface INotificacaoService
{
    Task AgendarNotificacaoAplicacaoAsync(Agendamento agendamento);
    Task CancelarNotificacaoAplicacaoAsync(int agendamentoId);
    Task EnviarToastAsync(string mensagem);
}
