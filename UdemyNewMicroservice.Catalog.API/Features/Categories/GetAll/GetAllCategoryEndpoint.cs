using Amazon.Runtime.Internal;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using UdemyNewMicroservice.Catalog.API.Features.Categories.Dtos;
using UdemyNewMicroservice.Catalog.API.Repositories;
using UdemyNewMicroservice.Shared;
using UdemyNewMicroservice.Shared.Extensions;

namespace UdemyNewMicroservice.Catalog.API.Features.Categories.GetAll
{

    public class GetAllCategoryQuery :IRequest<ServiceResult<List<CategoryDto>>>;
    
    public class GetAllCategoryQueryHandler(AppDbContext context):IRequestHandler<GetAllCategoryQuery, ServiceResult<List<CategoryDto>>>
    {
        public async Task<ServiceResult<List<CategoryDto>>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            var categories = await context.Categories.ToListAsync();
            var categoriesAsDto= categories.Select(c => new CategoryDto(c.Id, c.Name)).ToList();
            return ServiceResult<List<CategoryDto>>.SuccessAsOk(categoriesAsDto);
        }
    }

    public static class GetAllCategoryEndpoint
    {
        public static RouteGroupBuilder GetAllCategoryGroupEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/", async (IMediator mediator) =>
            {
                var result = await mediator.Send(new GetAllCategoryQuery());
                return result.ToGenericResult();
            });
            return group;
        }
    }
}
