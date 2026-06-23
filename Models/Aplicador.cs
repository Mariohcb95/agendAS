using AgendAS.Models.Enums;

namespace AgendAS.Models;

public class Aplicador
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Crf { get; set; } = string.Empty;
    public string Especialidade { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public StatusDisponibilidade StatusDisponibilidade { get; set; } = StatusDisponibilidade.Disponivel;

    public bool EstaDisponivel => StatusDisponibilidade == StatusDisponibilidade.Disponivel;
}
