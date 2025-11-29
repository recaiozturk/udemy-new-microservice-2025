using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using UdemyNewMicroservice.Catalog.API.Features.Courses;

namespace UdemyNewMicroservice.Catalog.API.Repositories
{
    public class CourseEntityConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToCollection("courses");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedNever(); //asla üretme , biz kendimiz üretcez snow flake, kütüphane ile
            builder.Property(c => c.Name).HasElementName("name").HasMaxLength(100);
            builder.Property(c => c.Description).HasElementName("description").HasMaxLength(1000);
            builder.Property(c => c.Created).HasElementName("cerated");
            builder.Property(c => c.UserId).HasElementName("userId");
            builder.Property(c => c.CategoryId).HasElementName("categoryId");
            builder.Property(c => c.Picture).HasElementName("picture");
            builder.Ignore(c => c.Category); //ilişkiyi mongo db de tutmayacağız,sql serverda otomatik ignor ediyor navigation propertyleri ancak mong odb de bu sekilde ignore etmemiz lazım

            //.net ef core da id si olmayan entity ici owner type olarak tanimlanir(ör: Feature Entity)
            builder.OwnsOne(c => c.Feature, fb =>
            {
                fb.HasElementName("feature");
                fb.Property(f => f.Duration).HasElementName("duration");
                fb.Property(f => f.Rating).HasElementName("rating");
                fb.Property(f => f.EducaterFullName).HasElementName("educaterFullName");
            });
        }
    }
}
