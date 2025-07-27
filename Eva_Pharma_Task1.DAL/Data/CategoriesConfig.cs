using Eva_Pharma_Task1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eva_Pharma_Task1.DAL.Data
{
    internal class CategoriesConfig : IEntityTypeConfiguration<Categories>
    {
        public void Configure(EntityTypeBuilder<Categories> builder)
        {

            builder.HasKey(c => c.Id);

            builder.Property(c => c.catName)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(c => c.catOrder)
                   .IsRequired();

            builder.Ignore(c => c.createdDate);

            builder.Property(c => c.markedAsDeleted)
                   .HasColumnName("isDeleted");

            var seedData = new List<Categories>();
            for (int i = 1; i <= 20; i++)
            {
                seedData.Add(new Categories
                {
                    Id = i,
                    catName = $"Category {i}",
                    catOrder = i,
                    markedAsDeleted = false
                });
            }

            builder.HasData(seedData);
        }
    }
}
