using System;

namespace AgendAS.Validators;

public static class LoginValidator
{
    public static (bool Valido, string Erro) Validar(string usuario, string senha)
    {
        if (string.IsNullOrWhiteSpace(usuario))
        {
            return (false, "O nome de usuário é obrigatório.");
        }

        if (string.IsNullOrWhiteSpace(senha))
        {
            return (false, "A senha é obrigatória.");
        }

        if (senha.Length < 4)
        {
            return (false, "A senha deve conter pelo menos 4 caracteres.");
        }

        return (true, string.Empty);
    }
}
