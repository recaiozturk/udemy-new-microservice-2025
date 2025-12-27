
using MediatR;
using UdemyNewMicroservice.Shared;

namespace UdemyNewMicroservice.Catalog.API.Features.Categories.Create
{
    public record  CreateCategoryCommand(string Name) :IRequest<ServiceResult<CreateCategoryResponse>>;




    //record ile inmotable objectlar oluşturabiliriz
    //yani nesne oluşturulduktan sonra değiştirilemez

    //yukardaki kısa süümüdür aşağıdaki uzun sürümüde yazabilirdik, .net 8 ile gelmiştir

    //public class CreateCategoryCommandHandler
    //{
    //    public string Name { get; init; }

    //    public CreateCategoryCommandHandler(string name)
    //    {
    //        Name = name;
    //    }
    //}
}
