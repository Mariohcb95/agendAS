namespace AgendAS.Helpers;

using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Helper estático para resolver serviços do container DI do MAUI.
/// Usado pelas Views que precisam de um construtor sem parâmetros (para DataTemplate do Shell)
/// e ainda assim obter seus ViewModels via injeção de dependência.
/// </summary>
public static class ServicoLocalizador
{
    private static IServiceProvider? _provedor;

    /// <summary>
    /// Inicializa o localizador com um provedor de serviços específico.
    /// </summary>
    public static void Inicializar(IServiceProvider provedor)
    {
        _provedor = provedor;
    }

    public static TServico Resolver<TServico>() where TServico : notnull
    {
        var provedor = _provedor ?? IPlatformApplication.Current?.Services;
        if (provedor == null)
        {
            throw new InvalidOperationException(
                $"[AgendAS] IServiceProvider não disponível ao resolver {typeof(TServico).Name}.");
        }

        return provedor.GetRequiredService<TServico>();
    }
}
