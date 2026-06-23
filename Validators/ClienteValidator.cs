using System;
using System.Text.RegularExpressions;
using AgendAS.Models;

namespace AgendAS.Validators;

public static class ClienteValidator
{
    public static (bool Valido, string Erro) Validar(Cliente cliente)
    {
        if (string.IsNullOrWhiteSpace(cliente.Nome))
        {
            return (false, "O nome do cliente é obrigatório.");
        }

        if (cliente.Nome.Trim().Length < 3)
        {
            return (false, "O nome do cliente deve conter pelo menos 3 caracteres.");
        }

        if (string.IsNullOrWhiteSpace(cliente.Cpf))
        {
            return (false, "O CPF do cliente é obrigatório.");
        }

        if (string.IsNullOrWhiteSpace(cliente.Telefone))
        {
            return (false, "O telefone do cliente é obrigatório.");
        }

        if (!string.IsNullOrWhiteSpace(cliente.Email))
        {
            // Validação simples de email
            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            if (!emailRegex.IsMatch(cliente.Email))
            {
                return (false, "O formato do e-mail é inválido.");
            }
        }

        return (true, string.Empty);
    }
}
