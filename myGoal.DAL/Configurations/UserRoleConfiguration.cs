using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using myGoal.Domain.Entity;

namespace myGoal.DAL.Configurations;

public class UserRoleConfiguration :IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        //тимчасовий код для заповнення таблці
        builder.HasData(new List<UserRole>()
        {
            new UserRole()
            {
                UserId = 1,
                RoleId = 2
            },
            new UserRole()
            {
                UserId = 2,
                RoleId = 1
            }

        });
    }
}