using System;
using System.Threading.Tasks;
using AgendAS.Models;
using AgendAS.Services.Interfaces;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Microsoft.Extensions.Logging;

namespace AgendAS.Services;

public class NotificacaoService : INotificacaoService
{
    private readonly ILogger<NotificacaoService> _logger;

    public NotificacaoService(ILogger<NotificacaoService> logger)
    {
        _logger = logger;
    }

    public async Task AgendarNotificacaoAplicacaoAsync(Agendamento agendamento)
    {
        _logger.LogInformation("Agendando notificação local para aplicação de {Medicamento} do cliente {Cliente} em {DataHora}", 
            agendamento.Medicamento, agendamento.Cliente.Nome, agendamento.DataHoraCompleta);

        // Simulamos o agendamento no log do sistema
        await Task.CompletedTask;
    }

    public async Task CancelarNotificacaoAplicacaoAsync(int agendamentoId)
    {
        _logger.LogInformation("Cancelando notificação local do agendamento Id: {Id}", agendamentoId);
        await Task.CompletedTask;
    }

    public async Task EnviarToastAsync(string mensagem)
    {
        _logger.LogInformation("Enviando Toast: {Mensagem}", mensagem);
        
        // Uso do Toast do CommunityToolkit.Maui
        try
        {
            var duracao = ToastDuration.Short;
            double tamanhoFonte = 14;
            var toast = Toast.Make(mensagem, duracao, tamanhoFonte);
            await toast.Show();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao disparar Toast visual. Fallback de console executado.");
        }
    }
}
