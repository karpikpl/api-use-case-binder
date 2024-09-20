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
    public int PathSegmentIndex { get; }

    public UseCaseAttribute(UseCaseName useCase, int pathSegmentIndex)
    {
        UseCase = useCase;
        PathSegmentIndex = pathSegmentIndex;
    }
}