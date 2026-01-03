using UdemyNewMicroservice.Catalog.API.Features.Categories.Create;

namespace UdemyNewMicroservice.Catalog.API.Features.Categories
{
    public static  class CategoryEndpointExt
    {
        public static void AddCategoryGroupEndpointExt(this WebApplication app)
        {
            app.MapGroup("api/categories").CreateCategoryGroupItemEnpoint();//buralarda devamında aynı controllerdaki gibi filterler vs eklyecez,tüm groupu kapsıaycaz

        }
    }
}
