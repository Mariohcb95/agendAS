using System;

namespace AgendAS.Models;

public class Cliente
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime DataNascimento { get; set; }
    public string Endereco { get; set; } = string.Empty;

    // Propriedade computada para Avatar (iniciais do nome)
    public string Iniciais
    {
        get
        {
            if (string.IsNullOrWhiteSpace(Nome)) return "?";
            var partes = Nome.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (partes.Length == 1) return partes[0][0].ToString().ToUpper();
            return (partes[0][0].ToString() + partes[^1][0].ToString()).ToUpper();
        }
    }
}
