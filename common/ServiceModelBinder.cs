using common;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;

public class ServiceModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext == null)
        {
            throw new ArgumentNullException(nameof(bindingContext));
        }

        // Access the attribute data
        var parameterDescriptor = bindingContext.ActionContext.ActionDescriptor.Parameters
            .FirstOrDefault(p => {
               var descriptor =  p as ControllerParameterDescriptor;
                return descriptor?.ParameterInfo.CustomAttributes.Any(a => a.AttributeType == typeof(FromServiceAttribute)) ?? false;
        });
        
        var fromServiceAttributes = (parameterDescriptor as ControllerParameterDescriptor)?.ParameterInfo
            .GetCustomAttributes(typeof(FromServiceAttribute), false);

         var fromServiceAttribute=    fromServiceAttributes?.FirstOrDefault() as FromServiceAttribute;
        
        var useCase = UseCaseProvider.GetUseCase(bindingContext.HttpContext);

        if (useCase == UseCaseName.None)
        {
            bindingContext.Result = ModelBindingResult.Failed();
            return Task.CompletedTask;
        }

        // need to get service bindingContext.ModelType for useCase
        // find which service implements the interface and has the UseCaseAttribute
        var implementationType = UseCaseExtensions.GetTypeForUseCase(bindingContext.ModelType, useCase);

        if (implementationType == null)
        {
            bindingContext.Result = ModelBindingResult.Failed();
            return Task.CompletedTask;
        }

        var service = bindingContext.HttpContext.RequestServices.GetService(implementationType);
        bindingContext.Result = ModelBindingResult.Success(service);
        return Task.CompletedTask;
    }
}