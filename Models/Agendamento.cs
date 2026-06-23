using System;
using AgendAS.Models.Enums;

namespace AgendAS.Models;

public class Agendamento
{
    public int Id { get; set; }
    public Cliente Cliente { get; set; } = new();
    public Aplicador Aplicador { get; set; } = new();
    public string Medicamento { get; set; } = string.Empty;
    public string LocalAplicacao { get; set; } = string.Empty;
    public DateTime Data { get; set; }
    public TimeSpan Hora { get; set; }
    public string DetalhesAplicacao { get; set; } = string.Empty;
    public string Observacoes { get; set; } = string.Empty;
    public StatusAgendamento Status { get; set; } = StatusAgendamento.Agendado;
    public DateTime DataCriacao { get; set; } = DateTime.Now;
    public DateTime DataAtualizacao { get; set; } = DateTime.Now;

    public DateTime DataHoraCompleta => Data.Date.Add(Hora);

    public string DescricaoResumo => $"{Medicamento} - {LocalAplicacao}";
}
