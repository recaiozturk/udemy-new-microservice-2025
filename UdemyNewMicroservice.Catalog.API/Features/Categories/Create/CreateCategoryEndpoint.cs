using MediatR;
using UdemyNewMicroservice.Shared.Extensions;
using UdemyNewMicroservice.Shared.Filters;

namespace UdemyNewMicroservice.Catalog.API.Features.Categories.Create
{
    public static class CreateCategoryEndpoint
    {
        public static RouteGroupBuilder CreateCategoryGroupItemEnpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/", async ( CreateCategoryCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return result.ToGenericResult();
            });

            group.AddEndpointFilter<ValidationFilter<CreateCategoryCommand>>(); //CreateCategoryCommand ı validate et

            return group;
        }
    }
}
