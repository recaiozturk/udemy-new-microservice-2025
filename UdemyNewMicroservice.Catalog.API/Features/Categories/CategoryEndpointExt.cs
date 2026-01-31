using UdemyNewMicroservice.Catalog.API.Features.Categories.Create;
using UdemyNewMicroservice.Catalog.API.Features.Categories.GetAll;

namespace UdemyNewMicroservice.Catalog.API.Features.Categories
{
    public static  class CategoryEndpointExt
    {
        public static void AddCategoryGroupEndpointExt(this WebApplication app)
        {
            app.MapGroup("api/categories")
                .CreateCategoryGroupItemEnpoint()
                .GetAllCategoryGroupEndpoint();
        }
    }
}
