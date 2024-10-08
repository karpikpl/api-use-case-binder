using System;

namespace common;

public enum UseCaseName
{
    None = 0,
    West,
    East,
}

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public sealed class UseCaseAttribute : Attribute
{
    public UseCaseName UseCase { get; }

    public UseCaseAttribute(UseCaseName useCase)
    {
        UseCase = useCase;
    }
}