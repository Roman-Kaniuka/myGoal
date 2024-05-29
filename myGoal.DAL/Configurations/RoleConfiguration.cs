using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using myGoal.Domain.Entity;
using myGoal.Domain.Enum;

namespace myGoal.DAL.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);

        //тимчасовий код для заповнення таблці
        builder.HasData(new List<Role>()
        {
            new Role()
            {
                Id = 1,
                Name = (nameof(Roles.User))
            },
            new Role()
            {
                Id = 2,
                Name = (nameof(Roles.Admin))
            },
            new Role()
            {
                Id = 3,
                Name = (nameof(Roles.Moderator))
            }
        });
    }
}