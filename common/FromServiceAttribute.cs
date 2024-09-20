using Microsoft.AspNetCore.Mvc.ModelBinding;

[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
public class FromServiceAttribute : Attribute, IBindingSourceMetadata
{
    public BindingSource BindingSource => BindingSource.Services;

    public int PathSegmentIndex {get;set;}
}