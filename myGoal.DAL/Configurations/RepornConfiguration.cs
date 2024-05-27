using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using myGoal.Domain.Entity;

namespace myGoal.DAL.Configurations;

public class RepornConfiguration : IEntityTypeConfiguration<Report>
{
    public void Configure(EntityTypeBuilder<Report> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Description).IsRequired().HasMaxLength(2000);
        
        //тимчасовий код для заповнення таблці
        builder.HasData(new List<Report>()
        {
            new Report()
            {
                Id = 1,
                Name = "test Roma",
                Description = "test",
                UserId = 1,
                CreateAt = DateTime.UtcNow,
                CreateBy = 1
            },
            new Report()
            {
                Id = 2,
                Name = "test Dima",
                Description = "test",
                UserId = 2,
                CreateAt = DateTime.UtcNow,
                CreateBy = 1
            }

        });
    }
}