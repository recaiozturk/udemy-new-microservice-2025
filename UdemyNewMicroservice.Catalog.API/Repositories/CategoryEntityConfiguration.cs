using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using UdemyNewMicroservice.Catalog.API.Features.Categories;

namespace UdemyNewMicroservice.Catalog.API.Repositories
{
    public class CategoryEntityConfiguration: IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToCollection("categories");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedNever(); //asla üretme , biz kendimiz üretcez snow flake, kütüphane ile
            builder.Property(c => c.Name).HasElementName("name").HasMaxLength(100);
            builder.Ignore(c => c.Courses); //ilişkiyi mongo db de tutmayacağız,sql serverda otomatik ignor ediyor navigation propertyleri ancak mong odb de bu sekilde ignore etmemiz lazım
        }
    }
}
