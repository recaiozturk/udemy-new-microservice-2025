using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UdemyNewMicroservice.Catalog.API.Repositories;
using UdemyNewMicroservice.Shared;

namespace UdemyNewMicroservice.Catalog.API.Features.Categories.Create
{
    public class CreateCategoryCommandHandler(AppDbContext context) : IRequestHandler<CreateCategoryCommand, ServiceResult<CreateCategoryResponse>>
    {
        public async Task<ServiceResult<CreateCategoryResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var existingCategory=await context.Categories.AnyAsync(x=>x.Name==request.Name,cancellationToken);

            if(existingCategory)
            {
                return ServiceResult<CreateCategoryResponse>.Error(
                    "Category Exists",
                    $"A category with the name '{request.Name}' already exists.",
                    System.Net.HttpStatusCode.BadRequest);
            }

            Category category = new Category
            {
                Name = request.Name,
                Id = NewId.NextSequentialGuid() //snowflake id ,birbirine benzer aynı zamanda artan guid ler üretir
            };

            await context.AddAsync(category,cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return ServiceResult<CreateCategoryResponse>.SuccessAsCreated(new CreateCategoryResponse(category.Id),"<empty>");

        }
    }
}
