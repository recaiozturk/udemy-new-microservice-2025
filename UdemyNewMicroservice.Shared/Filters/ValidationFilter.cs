using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace UdemyNewMicroservice.Shared.Filters
{
    public class ValidationFilter<T> : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            //minimal api da mvc deki gibi çok filterimiz yok , IEndpointFilter gibi az sayıda var 
            //IEndpointFilter minimal api de endpoint çalışamdan önce çalışır,araya girer

            var validator = context.HttpContext.RequestServices.GetRequiredService<IValidator<T>>(); //Miroservislerdeki validatörleri alıyoruz ör: CreateCategoryCommandValidator

             if(validator is null )
                return await next(context); //eğer validator yoksa devam et,endpoint çalışsın

            //parametrelerden entityModele ulaşıyoruz tipi T olan ör :  CreateCategoryEndpoint daki CreateCategoryCommand
            var requestModel = context.Arguments.OfType<T>().FirstOrDefault();

            if(requestModel is null)
                return await next(context); //eğer modelParamater yoksa devam et,endpoint çalışsın

            var validationResult = await validator.ValidateAsync(requestModel);

            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            return await next(context); //herşey yolundaysa endpoint çalışsın
        }
    }
}
