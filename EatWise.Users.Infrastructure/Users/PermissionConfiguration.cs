using EatWise.Users.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EatWise.Users.Infrastructure.Users;

internal sealed class PermissionConfiguration: IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("permissions");

        builder.HasKey(p => p.Code);

        builder.Property(p => p.Code).HasMaxLength(100);

        builder.HasData(
            Permission.GetUser,
            Permission.ModifyUser);

        builder
            .HasMany<Role>()
            .WithMany()
            .UsingEntity(joinBuilder =>
            {
                joinBuilder.ToTable("role_permissions");
                // Member permissions
                joinBuilder.HasData(CreateRolePermission(Role.Member, Permission.GetUser));
                joinBuilder.HasData(CreateRolePermission(Role.Member, Permission.ModifyUser));
                
                // Admin permissions
                CreateRolePermission(Role.Administrator, Permission.GetUser);
                CreateRolePermission(Role.Administrator, Permission.ModifyUser);
            });
    }

    private static object CreateRolePermission(Role role, Permission permission)
    {
        return new
        {
            RoleName = role.Name,
            PermissionCode = permission.Code
        };
    }
}
