using System.Reflection;

namespace common;

public static class UseCaseExtensions
{
    public static Type? GetTypeForUseCase(Type interfaceType, UseCaseName useCase)
    {
        var assembly = Assembly.GetEntryAssembly();

        var typesWithUseCaseAttribute = assembly!.GetTypes()
            .Where(t => t.GetCustomAttributes(typeof(UseCaseAttribute), true).Length > 0)
            .Where(t => interfaceType.IsAssignableFrom(t))
            .ToList();

        foreach (var type in typesWithUseCaseAttribute)
        {
            var useCaseAttribute = type.GetCustomAttributes(typeof(UseCaseAttribute), true).First() as UseCaseAttribute;

            if (useCaseAttribute!.UseCase == useCase)
            {
                return type;
            }
        }

        return null;
    }
}
