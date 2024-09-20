using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

public class ServiceModelBinderProvider : IModelBinderProvider
{
    public IModelBinder GetBinder(ModelBinderProviderContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (context.BindingInfo.BindingSource == BindingSource.Services)
        {
            return new BinderTypeModelBinder(typeof(ServiceModelBinder));
        }

        return null;
    }
}